using Godot;
using System;

public class FreezeTower : BaseTower
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene freezeProj= GD.Load<PackedScene>("res://src/scenes/FreezeTowerProjectile.tscn");

    protected override PackedScene ProjectileScene {get;} = GD.Load<PackedScene>("res://src/scenes/FreezeTowerProjectile.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }


}
