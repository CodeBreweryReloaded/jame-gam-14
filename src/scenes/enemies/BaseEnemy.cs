using Godot;
using System;

public abstract class BaseEnemy : KinematicBody2D, ITarget
{

    [Export]
    public int MaxHealth
    {
        get => healthBar.MaxHealth;
        set => healthBar.MaxHealth = value;
    }


    [Export]
    public int Health
    {
        get => healthBar.Health;
        set => healthBar.Health = value;
    }

    [Export]
    public int Bounty { get; set; } = 50;

    [Export]
    protected float BaseSpeed = 1.0F;

    protected bool IsFrozen { get; set; } = false;

    protected int SlowdownCount { get; set; } = 0;

    protected float Speed => IsFrozen ? 0 : BaseSpeed * (float)Math.Pow(0.7f, SlowdownCount);

    protected float healMultiplier = 1;

    [Export]
    public int DamageOnCastle { get; set; }

    [Export]
    public NodePath Target { get; set; }

    private Node2D _targetNode;

    public Node2D TargetNode
    {
        get
        {
            if (_targetNode == null && Target != null)
                _targetNode = GetNode<Node2D>(Target);
            return _targetNode;
        }
        set
        {
            _targetNode = value;
        }
    }

    private Lazy<HealthBar> healthBarLazy;

    protected HealthBar healthBar => healthBarLazy.Value;
    protected EnemyAudioPlayer enemyAudioPlayer;
    protected float AudioInterval = 5.0f;

    public BaseEnemy()
    {
        healthBarLazy = new Lazy<HealthBar>(() => GetNode<HealthBar>("HealthBar"));
    }

    [Signal]
    public delegate void OnCured(BaseEnemy enemy);


    [Signal]
    public delegate void onHitSignal(BaseEnemy self);

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    }


    public virtual void onHit(int heal, string effect, float effectDuration)
    {
        OverTimeEffectScene Ote = new OverTimeEffectScene();

        Health += (int)(heal * healMultiplier);
        if (Health >= MaxHealth)
        {
            Cured(); //TODO Mob cured
        }
        else
        {
            enemyAudioPlayer.PlayHurt();
        }

        switch (effect)
        {
            case "Slow":
                SlowdownCount++;
                Ote.OverTimeEffect("Slow", this, effectDuration);

                break;

            case "Freeze":
                IsFrozen = true;
                Ote.OverTimeEffect("Freeze", this, effectDuration);
                break;

            case "Multiplier":
                healMultiplier += 0.2f;
                break;
        }

        EmitSignal(nameof(onHitSignal), this);
    }

    protected virtual void Cured()
    {
        enemyAudioPlayer.PlayDeath();
        EmitSignal(nameof(OnCured), this);
        QueueFree();
    }

    private void OnEndEffect(string effect)
    {
        switch (effect)
        {
            case "Slow":
                SlowdownCount--;
                break;

            case "Freeze":
                IsFrozen = false;
                break;

            case "Multiplier":
                healMultiplier -= 0.2f;
                break;
        }
    }

    private void Buff()
    {
        BaseSpeed *= 1.2f;
    }

    private void DeBuff()
    {
        BaseSpeed *= (1 / 1.2f);
    }

    private void OnEntered()
    {
        Buff();
    }

    private void OnExited()
    {
        DeBuff();
    }

    protected void AudioQueue()
    {
        enemyAudioPlayer.PlayIdle();
    }
}
