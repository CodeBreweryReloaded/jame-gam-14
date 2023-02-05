using Godot;
using System;

public class RandomEffectPlayer2D : AudioStreamPlayer2D
{

    [Export(PropertyHint.ResourceType, "AudioStream")]
    public AudioStream[] Effects { get; set; }

    [Export]
    public bool RandomPitch { get; set; } = true;

    private AudioStreamRandomPitch randomPitch = new AudioStreamRandomPitch();

    public override void _Ready()
    {
        Connect("finished", this, nameof(changeSound));
        Play();
    }

    private void changeSound()
    {
        if (Effects.Length > 0)
        {
            int index = (int)Math.Round(GD.RandRange(0, Effects.Length - 1));
            AudioStream nextStream = Effects[index];
            if (RandomPitch)
            {
                nextStream = randomPitch;
                randomPitch.AudioStream = nextStream;
            } else {
                Stream = nextStream;
            }
        }
        Play();
    }

}
