using Godot;
using System;

public class FreezeTower : BaseTower
{
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
        EffectDuration++;
    }

}
