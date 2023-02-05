using Godot;
using System;

public class BattleUI : CanvasLayer
{
    private PackedScene currentTower;

    [Signal]
    delegate void towerSelected(PackedScene TowerType);
    [Signal]
    delegate void towerDeselected();

    private PlayerHealth playerHealth => GetNode<PlayerHealth>("PlayerHealth");

    public override void _Ready()
    {

    }

    public void Damage(int damage)
    {
        playerHealth.CurrentHealth -= damage;
        if (playerHealth.CurrentHealth <= 0)
        {
            GetTree().ChangeScene("res://src/scenes/ui/GameOverScreen.tscn");
        }
    }

    public void onTowerSelected(TowerButton towerButton)
    {
        currentTower = towerButton?.TowerType;
        foreach (Node node in GetChildren())
        {
            if (node is TowerButton tower)
            {
                tower.Active = tower == towerButton;
            }
        }

        if (towerButton == null)
        {
            EmitSignal(nameof(towerDeselected));
        }
        else
        {
            EmitSignal(nameof(towerSelected), towerButton.TowerType);
        }
    }
}
