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
            tower.Hide();
            towerAnchor.AddChild(tower);
            tower.Position = new Vector2(0,0);
        }
    }

    public void onTowerDeselected() {
        availability.Hide();
        tower = null;
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
        if (!reserved && IsInstanceValid(tower)) {
            Color mod = tower.Modulate;
            mod.a = 1.0f;
            tower.Modulate = mod;
            tower.Show();
            reserved = true;
            availability.toggleAvailability();
        }
    }
}
