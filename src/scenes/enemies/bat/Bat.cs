using Godot;
using System;

public class Bat : BaseEnemy
{

    public override void _Ready()
    {
        //LookAt(GetNode<Node2D>(Target).Position);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide(new Vector2(1, 0) * Speed);
    }

    public override void onHit(int heal, string effect, int duration){
        OverTimeEffectScene Ote = new OverTimeEffectScene();

        Health += heal;
        if (Health >= MaxHealth)
        {
            QueueFree(); //TODO Mob cured
        }

        switch (effect)
        {
            case "Slow":
                SlowdownCount++;
                Ote.OverTimeEffect("Slow", this, duration);

                break;

            case "Freeze":
                IsFrozen = true;
                Ote.OverTimeEffect("Freeze", this, duration);
                break;
        }

    }

    private void OnEndEffect(string effect){
        switch (effect)
        {
            case "Slow":
                SlowdownCount--;
                break;

            case "Freeze":
                IsFrozen = false;
                break;
        }
    }
}
