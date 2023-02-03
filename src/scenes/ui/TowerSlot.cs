using Godot;
using System;

public class TowerSlot : Control
{
    [Export(PropertyHint.ResourceType, "Texture")]
    private Texture sprite;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Sprite>("Sprite").Texture = sprite;
    }

    public void onButtonPressed() {
        GD.Print("click");
    }
}
