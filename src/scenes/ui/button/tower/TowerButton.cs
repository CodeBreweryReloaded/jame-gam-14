using Godot;
using System;

public class TowerButton : AnimatedButton
{
    [Export(PropertyHint.ResourceType, "Texture")]
    private Texture sprite;

    [Export(PropertyHint.ResourceType, "String")]
    private String towerType = "";

    [Signal]
    delegate void ButtonPressed(Texture texture);

    private bool active = false;

    public override void _Ready()
    {
        base._Ready();
        GetNode<Sprite>("Tower").Texture = sprite;
    }

    public override void onButtonPressed()
    {
        if (active) {
            EmitSignal(nameof(ButtonPressed), (Texture)null);
        } else {
            EmitSignal(nameof(ButtonPressed), sprite);
        }
        active = !active;
    }

}
