using Godot;
using System;

public class EnemyAudioPlayer : AudioStreamPlayer2D
{
    [Export]
    public AudioStreamRandomPitch[] DamageSounds { get; set; }

    public override void _Ready()
    {
    }

    public void OnHit(BaseEnemy self)
    {
        Play();
    }
}
