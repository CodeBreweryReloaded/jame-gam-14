using Godot;
using System;

public class BattleUI : CanvasLayer
{
    private PackedScene currentTower;

    [Signal]
    delegate void towerSelected(PackedScene TowerType);
    [Signal]
    delegate void towerDeselected();

    public override void _Ready()
    {
        
    }

    public void AddMoney(int amount) {
        GetNode<MoneyCounter>("MoneyCounter").addMoney(amount);
    }

    public void onTowerSelected(TowerButton towerButton) {
        currentTower = towerButton?.TowerType;
        foreach (Node node in GetChildren())
        {
            if (node is TowerButton tower) {
                tower.Active = tower == towerButton;
            }
        }

        if (towerButton == null) {
            EmitSignal(nameof(towerDeselected));
        } else {
            EmitSignal(nameof(towerSelected), towerButton.TowerType);
        }
    }
}
