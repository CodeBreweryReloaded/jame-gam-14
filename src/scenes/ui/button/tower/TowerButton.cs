using Godot;
using System;

public class TowerButton : AnimatedButton
{
    [Export(PropertyHint.ResourceType, "Texture")]
    private Texture towerSprite;

    [Export]
    private PackedScene towerType;

    [Signal]
    delegate void ButtonPressed(Texture texture);

    [Signal]
    delegate void TowerSelected(TowerButton tower);

    private bool active = false;

    public bool Active {
        get => active;
        set => active = value;
    }

    public PackedScene TowerType {
        get => towerType;
    }

    public override void _Ready()
    {
        base._Ready();
        GetNode<Sprite>("Tower").Texture = towerSprite;
    }

    public override void onButtonPressed()
    {
        if (active) {
            EmitSignal(nameof(ButtonPressed), (Texture)null);
            EmitSignal(nameof(TowerSelected), (TowerButton)null);
        } else {
            EmitSignal(nameof(ButtonPressed), towerSprite);
            EmitSignal(nameof(TowerSelected), this);
        }
    }

}
