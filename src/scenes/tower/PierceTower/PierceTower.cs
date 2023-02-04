using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class PierceTower : BaseTower
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    protected override PackedScene ProjectileScene => GD.Load<PackedScene>("res://src/scenes/PierceTowerProjectile.tscn");

    [Export]
    public int Pierce = 2;
    private HashSet<Node2D> HitList = new HashSet<Node2D>();

    public override void _Ready()
    {
        base._Ready();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    protected override void LevelUp(){//TODO is level up a tower's responsibility?
		Level++; 
		Heal = (int) (Heal * LevelFactor);
        Pierce++;
	}


}
