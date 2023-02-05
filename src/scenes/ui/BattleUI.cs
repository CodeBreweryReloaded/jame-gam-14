using Godot;
using System;

public class BattleUI : CanvasLayer
{
    private PackedScene currentTower;

    [Signal]
    delegate void towerSelected(PackedScene TowerType);
    [Signal]
    delegate void towerDeselected();

    [Signal]
    public delegate void StartWave();

    private PlayerHealth playerHealth => GetNode<PlayerHealth>("PlayerHealth");

    private Control nextRoundButton;

    public override void _Ready()
    {
        nextRoundButton = GetNode<Control>("NextRoundButton");
    }

    public void Damage(int damage)
    {
        playerHealth.CurrentHealth -= damage;
        if (playerHealth.CurrentHealth <= 0)
        {
            GetTree().ChangeScene("res://src/scenes/ui/GameOverScreen.tscn");
        }
    }

    public void AddMoney(int amount)
    {
        GetNode<MoneyCounter>("MoneyCounter").addMoney(amount);
    }

    public void SpendMoney(int amount)
    {
        GetNode<MoneyCounter>("MoneyCounter").spendMoney(amount);
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

    public void WaveFinished()
    {
        nextRoundButton.Visible = true;
    }

    public void StartWaveListener()
    {
        nextRoundButton.Visible = false;
        EmitSignal(nameof(StartWave));
    }

    public void GameWon()
    {
        if (playerHealth.CurrentHealth > 0)
        {
            GetTree().ChangeScene("res://src/scenes/ui/WinnerScreen.tscn");
        }
    }
}
