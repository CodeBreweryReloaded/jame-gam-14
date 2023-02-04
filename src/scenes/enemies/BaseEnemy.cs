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
    protected float MaxSpeed = 1.0F;

    [Export]
    protected float Speed;

    [Export]
    protected NodePath Target;


	private Lazy<HealthBar> healthBarLazy;

	protected HealthBar healthBar => healthBarLazy.Value;

	public BaseEnemy()
	{
		healthBarLazy = new Lazy<HealthBar>(() => GetNode<HealthBar>("HealthBar"));
	}

	public override void _Ready()
	{
		base._Ready();

		// set the speed to the max speed
		Speed = MaxSpeed;
	}
    
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    }

    public abstract void onHit(int heal, string effect, int duration);
}