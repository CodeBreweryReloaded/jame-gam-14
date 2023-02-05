using Godot;
using System;

public class Bat : BaseEnemy
{

    Vector2 currentPosition{get; set;}
    private Vector2 targetVector = new Vector2();

    public override void _Ready()
    {
        currentPosition = GlobalPosition;
        targetVector = TargetNode.Position - currentPosition;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        //MoveAndSlide(new Vector2(1, 0) * Speed);

        Vector2 nextVelocity = targetVector.Normalized() * Speed * delta;
        GlobalPosition += nextVelocity;
    }
}
