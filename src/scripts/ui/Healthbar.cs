using Godot;
using System;

public class Healthbar : AnimatedSprite
{
    [Export]
    public int MaxHealth { get; set; }

    [Export]
    public int Health { get; set; }


    public override void _Ready()
    {
        this.Playing = false;
    }

    public override void _Process(float delta)
    {
        int frame_count = this.Frames.GetFrameCount(this.Animation);
        this.Frame = (int)Math.Round((double)(Health / MaxHealth) * frame_count);
    }
}
