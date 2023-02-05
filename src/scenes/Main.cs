using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    private Lazy<BattleUI> ui;

    private List<Pedestal> pedestals;

    protected BattleUI UI => ui.Value;

    [Signal]
    delegate void towerSelected(PackedScene towerType);
    [Signal]
    delegate void towerDeselected();

    public Main()
    {
        ui = new Lazy<BattleUI>(() => GetNode<BattleUI>("BattleUI"));
    }

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        foreach (Node node in GetChildren())
        {
            if (node is Pedestal pedestal) {
                Connect(nameof(towerSelected), pedestal, nameof(Pedestal.onTowerSelected));
                Connect(nameof(towerDeselected), pedestal, nameof(Pedestal.onTowerDeselected));
                pedestal.Connect(nameof(Pedestal.TowerSet), this, nameof(onTowerSet));
            }
        }

        GetNode<Spawner>("Spawner").Connect(nameof(Wave.EnemySpawned), this, nameof(onSpawned));
    }

    private void onSpawned(BaseEnemy enemy) {
        enemy.Connect(nameof(BaseEnemy.OnCured), this, nameof(onCured));
    }

    private void onCured(BaseEnemy enemy) {
        UI.AddMoney(enemy.Bounty);
    }

    private void onTowerSelected(PackedScene towerType) {
        EmitSignal(nameof(towerSelected), towerType);
    }

    private void onTowerDeselected() {
        EmitSignal(nameof(towerDeselected));
    }

    private void onTowerSet(Pedestal pedestal, BaseTower tower) {
        try {
            UI.SpendMoney(tower.Cost);
            pedestal.SetTower(tower);
        } catch {
        }
    }
}
