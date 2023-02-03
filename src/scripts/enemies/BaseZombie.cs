using Godot;
using System;

public class BaseZombie : KinematicBody2D
{
    [Export]
    private int MaxHealth = 10;

    [Export]
    private int Health = 1;

    [Export]
    private float Speed = 1.0F;

    [Export]
    private NodePath Target;

    private NavigationAgent2D agent;

    public override void _Ready()
    {
        base._Ready();

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
        GD.Print("Hit! Health: " + Health);
        Health += 1;
        if (Health >= MaxHealth)
        {
            QueueFree();
        }
    }

}
