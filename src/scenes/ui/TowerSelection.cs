using Godot;
using System;

public class TowerSelection : CanvasLayer
{
    private String currentTower;

    public override void _Ready()
    {
        
    }

    public void onTowerSelected(TowerButton towerButton) {
        if (towerButton != null) {
            currentTower = towerButton.TowerType;
        }
        foreach (Node node in GetChildren())
        {
            if (node is TowerButton) {
                TowerButton tower = (TowerButton)node;
                tower.Active = tower == towerButton ? true : false;
            }
        }
    }
}
