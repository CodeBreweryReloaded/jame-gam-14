using Godot;
using System;

public class HealTower : BaseTower
{
    protected override PackedScene ProjectileScene => GD.Load<PackedScene>("res://src/scenes/HealTowerProjectile.tscn");

    public override void _Ready()
    {
        base._Ready();
    }
}
