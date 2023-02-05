using Godot;
using System;

public class TitleScreen : CanvasLayer
{
    private AnimatedSprite sprite;
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public void hover() {
        sprite.Animation = "Hover";
    }

    public void unhover() {
        sprite.Animation = "Normal";
    }

    public void click() {
        GetTree().ChangeScene("res://src/scenes/Main.tscn");
    }
}
