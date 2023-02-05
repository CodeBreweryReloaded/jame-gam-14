using Godot;
using System;

public class FlagZombie : KinematicBody2D
{
    [Export]
    private int Range = 10;
    [Export(PropertyHint.ResourceType, "NodePath")]
    private NodePath colliderPath;

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
}
