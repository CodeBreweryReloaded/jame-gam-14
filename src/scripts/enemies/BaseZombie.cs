using Godot;
using System;

public class BaseZombie : KinematicBody2D
{
    [Export]
    private int MaxHealth = 10;

    [Export]
    private int Health = 1;

    [Export]
    private float MaxSpeed = 1.0F;

    [Export]
    private float Speed;

    [Export]
    private NodePath Target;

    private NavigationAgent2D agent;

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
