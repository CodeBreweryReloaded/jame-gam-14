using Godot;
using System;

public class Bat : BaseEnemy
{

    Vector2 currentPosition{get; set;}
    private Vector2 targetVector = new Vector2();

    public override void _Ready()
    {
        base._Ready();
        currentPosition = GlobalPosition;
        targetVector = TargetNode.Position - currentPosition;
        enemyAudioPlayer = GetNode<EnemyAudioPlayer>("AudioStreamPlayer2D");
        Timer timer = new Timer();
        timer.Autostart = true;
        timer.OneShot = false;
        timer.WaitTime = AudioInterval;
        timer.Connect("timeout", this, nameof(AudioQueue));
        AddChild(timer);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        //MoveAndSlide(new Vector2(1, 0) * Speed);

        Vector2 nextVelocity = targetVector.Normalized() * Speed * delta;
        GlobalPosition += nextVelocity;
    }
}
