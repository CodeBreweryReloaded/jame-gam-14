using Godot;
using System;

public class MultiplierTower : BaseTower
{
    protected override PackedScene ProjectileScene => GD.Load<PackedScene>("res://src/scenes/MultiplierTowerProjectile.tscn");

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public override void _Ready()
    {
        base._Ready();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
