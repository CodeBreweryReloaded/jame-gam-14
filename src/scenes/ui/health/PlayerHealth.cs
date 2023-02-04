using Godot;
using System;

public class PlayerHealth : Sprite
{

    private int maxHealth;
    private int currentHealth;
    private Color color;

    [Export(PropertyHint.ResourceType, "Int")]
    private int width;
    
    [Export(PropertyHint.ResourceType, "Int")]
    private int height;

    [Export]
    public int MaxHealth
    {
        get => maxHealth;
        set { maxHealth = value; Update(); }
    }

    [Export]
    public int CurrentHealth
    {
        get => currentHealth;
        set {currentHealth = value; Update();}
    }

    [Export]
    public Color Color
    {
        get => color;
        set
        {
            color = value;
            Update();
        }
    }


    public override void _Draw()
    {
        float newWidth = (currentHealth / (float)MaxHealth) * width;
        newWidth = Mathf.Clamp(newWidth, 0, width);
        DrawRect(new Rect2(0, 0, newWidth, height), color);
    }


    public void damageTaken(int damage) {
        CurrentHealth -= damage;
    }

}
