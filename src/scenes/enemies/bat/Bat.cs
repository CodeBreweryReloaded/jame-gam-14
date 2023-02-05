using Godot;
using System;

public class Bat : BaseEnemy
{

    public override void _Ready()
    {
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

        MoveAndSlide(new Vector2(1, 0) * Speed);
    }
}
