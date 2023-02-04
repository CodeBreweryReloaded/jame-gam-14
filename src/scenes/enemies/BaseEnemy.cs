using Godot;
using System;

public abstract class BaseEnemy : KinematicBody2D
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
    protected float BaseSpeed = 1.0F;

    protected bool IsFrozen { get; set; } = false;

    protected int SlowdownCount { get; set; } = 0;

    protected float Speed => IsFrozen ? 0 : BaseSpeed * (float)Math.Pow(0.7f, SlowdownCount);

    [Export]
    protected NodePath Target;


	private Lazy<HealthBar> healthBarLazy;

	protected HealthBar healthBar => healthBarLazy.Value;

	public BaseEnemy()
	{
		healthBarLazy = new Lazy<HealthBar>(() => GetNode<HealthBar>("HealthBar"));
	}
    
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    }

    public virtual void onHit(int heal, string effect, float effectDuration){
        OverTimeEffectScene Ote = new OverTimeEffectScene();

        Health += heal;
        if (Health >= MaxHealth)
        {
            QueueFree(); //TODO Mob cured
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
        }

    }

    private void OnEndEffect(string effect){
        switch (effect)
        {
            case "Slow":
                SlowdownCount--;
                break;

            case "Freeze":
                IsFrozen = false;
                break;
        }
    }
}