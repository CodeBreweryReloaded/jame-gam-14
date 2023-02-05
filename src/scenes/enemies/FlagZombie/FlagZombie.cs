using Godot;
using System;
using System.Collections.Generic;

public class FlagZombie : BaseEnemy
{
    [Export]
    private int Range = 10;

    [Export(PropertyHint.ResourceType, "NodePath")]
    private NodePath colliderPath;
    private HashSet<Node2D> InRangeList = new HashSet<Node2D>();

    [Signal]
    protected delegate void Entered();
    [Signal]
    protected delegate void Exited();

    public override void _Ready()
    {
        CollisionShape2D shape = new CollisionShape2D();
        CircleShape2D circle = new CircleShape2D();
        circle.Radius = Range;
        shape.Shape = circle;
        GetNode<Node2D>(colliderPath).AddChild(shape);
        Update();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

    }


    protected virtual void _on_BuffRange_body_entered(Node2D body)
    {
        if (body is BaseEnemy)
        {
            InRangeList.Add(body);
            Connect(nameof(Entered), body, "OnEntered");
            EmitSignal(nameof(Entered));
        }
        
    }

    protected virtual void _on_BuffRange_body_exited(Node2D body)
    {

        if (body is BaseEnemy)
        {
            InRangeList.Remove(body);
            Connect(nameof(Exited), body, "OnExited");
            EmitSignal(nameof(Exited));
        }
    }
}
