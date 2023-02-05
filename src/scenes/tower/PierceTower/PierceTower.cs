using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class PierceTower : BaseTower
{
    [Export]
    public int Pierce = 2;
    private HashSet<Node2D> HitList = new HashSet<Node2D>();

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void LevelUp(){
		Level++; 
		Heal = (int) (Heal * LevelFactor);
        Pierce++;
	}


}
