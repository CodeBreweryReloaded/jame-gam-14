using Godot;
using System;

public class Zombie : BaseEnemy
{
    protected NavigationAgent2D agent;
    protected Node2D anchor;

    [Export(PropertyHint.ResourceType, "NodePath")]
    protected NodePath agentPath;

    [Export(PropertyHint.ResourceType, "NodePath")]
    protected NodePath anchorPath;

    [Export(PropertyHint.ResourceType, "Double")]
    protected double randomRange = 1.0f;

    public override void _Ready()
    {
        base._Ready();
        agent = GetNode<NavigationAgent2D>(agentPath);
        anchor = GetNode<Node2D>(anchorPath);
        agent.SetTargetLocation(TargetNode.GlobalPosition);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (agent.IsTargetReached()) return;

        Vector2 currentPosition = anchor.GlobalPosition;
        Vector2 nextPathPosition = agent.GetNextLocation();
        Vector2 nextVelocity = (nextPathPosition - currentPosition).Normalized() * Speed;

        MoveAndSlide(nextVelocity);
    }
}
