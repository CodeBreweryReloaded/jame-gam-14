using Godot;
using System;
using System.Collections.Generic;


public class PierceTowerProjectile : BaseProjectile
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private int MaxPierce => ((PierceTower)Tower).Pierce;
    private int PierceCount = 0;
    private HashSet<Node2D> HitList = new HashSet<Node2D>();
    private Vector2 targetVector = new Vector2();
    private bool TargetAquired = false;
    [Export]
    private Texture texture;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Sprite sprite = new Sprite();
        sprite.Texture = texture;
        AddChild(sprite);
        base._Ready();
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 currentPosition = GlobalPosition;

        if (IsInstanceValid(Target) && !TargetAquired)
        {
            targetVector = Target.Position - currentPosition;
            TargetAquired = true;
        }

        Vector2 nextVelocity = targetVector.Normalized() * Tower.ProjectileSpeed * delta;
        GlobalPosition += nextVelocity;

        if (!visibility.IsOnScreen())
        {
            QueueFree();
        }
    }


    public override void _on_Area2D_body_entered(Node2D body)
    {
        if (HitList.Add(body))
        {
            PierceCount++;
        }
        if (PierceCount >= MaxPierce)
        {
            QueueFree();
        }

        Connect(nameof(Hit), body, "onHit");
        EmitSignal(nameof(Hit), Tower.Heal, Tower.Effect, Tower.EffectDuration);

    }
}
