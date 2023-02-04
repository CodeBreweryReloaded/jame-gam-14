using Godot;
using System;

public class Bat : BaseEnemy
{

    public override void _Ready()
    {
        //LookAt(GetNode<Node2D>(Target).Position);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide(new Vector2(1, 0) * Speed);
    }

    public override void onHit(int heal, string effect)
    {
        QueueFree();
    }
}
