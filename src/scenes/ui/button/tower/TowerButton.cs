using Godot;
using System;

public class TowerButton : AnimatedButton
{
    [Export(PropertyHint.ResourceType, "Texture")]
    private Texture sprite;

    [Export(PropertyHint.ResourceType, "String")]
    private String towerType = "";

    [Signal]
    delegate void ButtonPressed();

    public override void _Ready()
    {
        base._Ready();
        GetNode<Sprite>("Tower").Texture = sprite;
    }

    public override void onButtonPressed()
    {
        EmitSignal(nameof(ButtonPressed), towerType);
    }

}
