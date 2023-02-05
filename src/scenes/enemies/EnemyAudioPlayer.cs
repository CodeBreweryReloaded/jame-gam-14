using Godot;
using System;

public class EnemyAudioPlayer : AudioStreamPlayer2D
{
    [Export(PropertyHint.ResourceType, "AudioStream")]
    public AudioStream[] HurtSounds { get; set; }

    [Export]
    public AudioStream[] IdleSounds { get; set; }
    [Export]
    public AudioStream[] DeathSounds { get; set; }
    [Export]
    public bool RandomPitch { get; set; } = true;

    private AudioStreamRandomPitch randomPitch = new AudioStreamRandomPitch();

    public override void _Ready()
    {
        base._Ready();
    }

    public void PlayHurt(BaseEnemy self)
    {
        int index = (int)Math.Round(GD.RandRange(0, HurtSounds.Length - 1));
        AudioStream nextStream = HurtSounds[index];
        if (RandomPitch)
        {
            randomPitch.AudioStream = nextStream;
        }
        else
        {
            Stream = nextStream;
        }
        Play();

    }

    public void PlayIdle()
    {
        int index = (int)Math.Round(GD.RandRange(0, IdleSounds.Length - 1));
        GD.Print(IdleSounds[index]);
        AudioStream nextStream = IdleSounds[index];
        if (RandomPitch)
        {
            randomPitch.AudioStream = nextStream;
        }
        else
        {
            Stream = nextStream;
        }
        Play();
    }

    public void PlayDeath()
    {
        if (RandomPitch)
        {
            randomPitch.AudioStream = IdleSounds[0];
        }
        else
        {
            Stream = IdleSounds[0];
        }
        Play();

    }

}
