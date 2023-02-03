using Godot;
using System;

public class BaseZombie : KinematicBody2D
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
	private float MaxSpeed = 1.0F;

	[Export]
	private float Speed;

	[Export]
	private NodePath Target;

	private NavigationAgent2D agent;

	private Lazy<HealthBar> healthBarLazy;

	private HealthBar healthBar => healthBarLazy.Value;

	public BaseZombie()
	{
		healthBarLazy = new Lazy<HealthBar>(() => GetNode<HealthBar>("HealthBar"));
	}

	public override void _Ready()
	{
		base._Ready();

		// set the speed to the max speed
		Speed = MaxSpeed;

		agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		agent.SetTargetLocation(GetNode<Node2D>(Target).GlobalPosition);
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);

		if (agent.IsTargetReached()) return;

		Vector2 currentPosition = GlobalTransform.origin;
		Vector2 nextPathPosition = agent.GetNextLocation();

		Vector2 nextVelocity = (nextPathPosition - currentPosition).Normalized() * Speed;

		MoveAndSlide(nextVelocity);
	}


	public void OnHit()
	{
		Health += 1;
		if (Health >= MaxHealth)
		{
			QueueFree();
		}
	}

}
