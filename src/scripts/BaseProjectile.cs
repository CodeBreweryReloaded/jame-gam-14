using Godot;
using System;

public class BaseProjectile : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    protected Vector2 Velocity = new Vector2(10, 0);
    [Export]
    protected Node2D Target { get; set; }
    [Export]
    protected float Heal { get; set; }
    [Export]
    protected string Effekt { get; set; }
    [Signal]
    protected delegate void Hit();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        LookAt(Target.GlobalPosition);
        Position += Velocity * delta;
    }

    public void _on_Area2D_body_entered(Node2D body)
    {
        Hide(); // Player disappears after being hit.

        Connect(nameof(Hit), body, "onHit");
        EmitSignal(nameof(Hit), Heal, Effekt);

        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }


}
