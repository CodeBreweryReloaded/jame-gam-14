using Godot;
using System;

public class MoneyCounter : AnimatedSprite
{
    private int money = 1000;

    [Export]
    private NodePath textPath;
    private Label text;

    public int Money {
        get => money;
        set {
            money = value;
            text.Text = money.ToString();
        }
    } 

    public override void _Ready()
    {
        text = GetNode<Label>(textPath);
        text.Text = money.ToString();
    }

    public void spendMoney(int spent) {
        if (money - spent < 0) {
            throw new ArgumentException();
        }
        Money -= spent;
    }

    public void addMoney(int added) {
        Money += added;
    }
}
