using Godot;
using System;

public class MultiplierTower : BaseTower
{

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void LevelUp(){//TODO is level up a tower's responsibility?
		Level++; 
		Heal = (int) (Heal * LevelFactor);
        EffectDuration++;
	}
}
