using Godot;
using System;

public class BattleUI : CanvasLayer
{
    private String currentTower;

    public override void _Ready()
    {
        
    }

    public void onTowerSelected(TowerButton towerButton) {
        currentTower = towerButton?.TowerType;
        foreach (Node node in GetChildren())
        {
            if (node is TowerButton tower) {
                tower.Active = tower == towerButton;
            }
        }
    }
}
