using Godot;
using System;

public class WinnerScreen : CanvasLayer
{
    public void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

    public void OnRetryButtonPressed()
    {
        GetTree().ChangeScene("res://src/scenes/Main.tscn");
    }
}
