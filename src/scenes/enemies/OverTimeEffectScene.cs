using Godot;
using System;

public class OverTimeEffectScene : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private string Effect;
    private BaseEnemy AffectedEnemy;
    [Signal]
    protected delegate void EndEffect();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void OverTimeEffect(string effect, BaseEnemy affectedEnemy, float effectDuration)
    {
        Effect = effect;
        AffectedEnemy = affectedEnemy;

        Timer timer = new Timer();
        timer.Autostart = true;
        timer.OneShot = true;
        timer.WaitTime = effectDuration;
        timer.Connect("timeout", this, nameof(onTimeOut));
        AffectedEnemy.AddChild(timer);

    }

    private void onTimeOut()
    {
        Connect(nameof(EndEffect), AffectedEnemy, "OnEndEffect");
        EmitSignal(nameof(EndEffect), Effect);
        QueueFree();
    }
}
