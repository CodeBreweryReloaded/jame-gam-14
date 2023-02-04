using Godot;
using System;

public class OverTimeEffectScene : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    protected delegate void EndEffect(string effect, BaseZombie effectedEnemy);
    private string Effect;
    private BaseZombie AffectedEnemy;




    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void OvertTimeEffect(string effect, BaseZombie affectedEnemy, int duration)
    {
        Effect = effect;
        AffectedEnemy = affectedEnemy;

        Timer timer = new Timer();
        timer.Autostart = true;
        timer.OneShot = true;
        timer.WaitTime = duration;
        Connect("timeout", this, "onTimeOut");
        AddChild(timer);

    }


    private void onTimeOut()
    {
        EmitSignal(nameof(EndEffect), Effect, AffectedEnemy);
        QueueFree();
    }
}
