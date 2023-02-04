using Godot;
using System;

public class PauseButton : AnimatedButton
{

    public override void _Ready()
    {
        base._Ready();
    }

    public override void onButtonPressed()
    {
        GetTree().Paused = !GetTree().Paused;
    }
}
