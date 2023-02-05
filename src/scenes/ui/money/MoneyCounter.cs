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
            money = Math.Min(value, (int)Math.Pow(10, MaxDigitCount) - 1);
            text.Text = money.ToString();
        }
    }

    public int MaxDigitCount { get; set; } = 5;

    public override void _Ready()
    {
        text = GetNode<Label>(textPath);
        Money = Money;
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
