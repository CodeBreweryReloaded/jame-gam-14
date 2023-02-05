using Godot;
using System;

public class Pedestal : Node2D
{
    [Export]
    private float towerOpacity = 0.5f;

    [Export]
    private NodePath towerAnchorPath;
    private Node2D towerAnchor;

    [Export]
    private NodePath availabilityPath;
    private Selection availability;

    private BaseTower tower;

    private bool reserved = false;

    [Signal]
    public delegate void TowerSet(Pedestal pedestal, BaseTower tower);

    public override void _Ready()
    {
        availability = GetNode<Selection>(availabilityPath);
        towerAnchor = GetNode<Node2D>(towerAnchorPath);
        availability.Hide();
    }

    public void onTowerSelected(PackedScene towerType) {
        if (!reserved) {
            availability.Show();
            if (IsInstanceValid(tower)) {
                tower.QueueFree();
            }
            tower = towerType.Instance<BaseTower>();
            Color mod = tower.Modulate;
            mod.a = towerOpacity;
            tower.Modulate = mod;
            tower.canShoot = false;
            tower.Hide();
            towerAnchor.AddChild(tower);
            tower.Position = new Vector2(0,0);
        }
    }

    public void onTowerDeselected() {
        availability.Hide();
        tower = null;
    }

    public void SetTower(BaseTower tower) {
        if (!reserved && IsInstanceValid(tower)) {
            tower.canShoot = true;
            Color mod = tower.Modulate;
            mod.a = 1.0f;
            tower.Modulate = mod;
            tower.Show();
            reserved = true;
            availability.toggleAvailability();
        }
    }

    private void onHover() {
        if (IsInstanceValid(tower)) {
            tower.Show();
        }
    }

    private void onUnhover() {
        if (IsInstanceValid(tower) && !reserved) {
            tower.Hide();
        }
    }

    private void onTowerSet() {
        EmitSignal(nameof(TowerSet), this, tower);
    }
}
