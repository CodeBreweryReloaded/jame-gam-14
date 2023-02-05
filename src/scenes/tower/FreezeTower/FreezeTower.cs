using Godot;
using System;

public class FreezeTower : BaseTower
{
    public override void _Ready()
    {
        base._Ready();
    }

    protected override void LevelUp(){
        Level++; 
        Heal = (int) (Heal * LevelFactor);
        EffectDuration++;
    }

}
