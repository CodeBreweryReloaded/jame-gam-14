using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    private List<Pedestal> pedestals;

    [Signal]
    delegate void towerSelected(PackedScene towerType);
    [Signal]
    delegate void towerDeselected();

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        foreach (Node node in GetChildren())
        {
            if (node is Pedestal pedestal) {
                Connect(nameof(towerSelected), pedestal, nameof(Pedestal.onTowerSelected));
                Connect(nameof(towerDeselected), pedestal, nameof(Pedestal.onTowerDeselected));
            }
        }
    }

    private void onTowerSelected(PackedScene towerType) {
        EmitSignal(nameof(towerSelected), towerType);
    }

    private void onTowerDeselected() {
        EmitSignal(nameof(towerDeselected));
    }
}
