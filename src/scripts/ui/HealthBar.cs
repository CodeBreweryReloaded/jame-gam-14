using Godot;
using System;

public class HealthBar : Node2D
{
    private const int WIDTH = 16;
    private const int HEIGHT = 4;

    private int _maxHealth;

    [Export]
    public int MaxHealth
    {
        get => _maxHealth;
        set { _maxHealth = value; Update(); }
    }

    private int _health;

    [Export]
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            Update();
        }
    }

    private Color _color;
    [Export]
    public Color color
    {
        get => _color;
        set
        {
            _color = value;
            Update();
        }
    }


    public override void _Draw()
    {
        float width = (Health / (float)MaxHealth) * WIDTH;
        width = Mathf.Clamp(width, 0, WIDTH);
        DrawRect(new Rect2(0, 0, width, HEIGHT), _color);
    }
}
