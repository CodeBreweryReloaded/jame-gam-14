using Godot;
using System;
using System.Collections.Generic;

public class FlagZombie : Zombie
{
    [Export]
    private int BuffRange = 10;
    [Export(PropertyHint.ResourceType, "NodePath")]
    private NodePath colliderPath;
    [Signal]
    protected delegate void Entered();
    [Signal]
    protected delegate void Exited();

    public override void _Ready()
    {
        CollisionShape2D shape = new CollisionShape2D();
        CircleShape2D circle = new CircleShape2D();
        circle.Radius = BuffRange;
        shape.Shape = circle;
        GetNode<Node2D>(colliderPath).AddChild(shape);
        agent = GetNode<NavigationAgent2D>(agentPath);
        anchor = GetNode<Node2D>(anchorPath);
        agent.SetTargetLocation(GetNode<Node2D>(Target).GlobalPosition);
        Update();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

    }


    protected virtual void _on_BuffRangeNode_body_entered(Node2D body)
    {
        if (body is BaseEnemy  && !(body is FlagZombie))
        {
            Connect(nameof(Entered), body, "OnEntered");
            EmitSignal(nameof(Entered));
        }
        
    }

    protected virtual void _on_BuffRangeNode_body_exited(Node2D body)
    {

        if (body is BaseEnemy && !(body is FlagZombie))
        {
            Connect(nameof(Exited), body, "OnExited");
            EmitSignal(nameof(Exited));
        }
    }
}
