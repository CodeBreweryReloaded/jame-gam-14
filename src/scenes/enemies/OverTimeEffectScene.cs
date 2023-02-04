using Godot;
using System;

public class OverTimeEffectScene : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
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

    public void OvertTimeEffect(string Effect, BaseZombie effectedEnemy)
    {
        Timer timer = new Timer();
        timer.Autostart = true;
        timer.OneShot = true;
        timer.WaitTime = 3;
        AddChild(timer);

    }


    private void onTimeOut(string effect, BaseZombie effectedEnemy){

    }
}
