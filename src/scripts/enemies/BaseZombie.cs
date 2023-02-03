using Godot;
using System;

public class BaseZombie : Node2D
{
    [Export]
    public int MaxHealth = 10;

    [Export]
    public int Health = 1;

    [Export]
    public float Speed = 1.0F;

    [Export]
    public BaseZombie Test;


    public override void _Ready()
    {
    }


    public void OnHit()
    {
        GD.Print("Hit! Health: " + Health);
        Health += 1;
        if (Health >= MaxHealth)
        {
            QueueFree();
        }
    }

}
