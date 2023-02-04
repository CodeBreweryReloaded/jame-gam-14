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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }


public override void _on_Area2D_body_entered(Node2D body)
    {
        if(HitList.Add(body)){
            PierceCount++;
        }
        if(PierceCount >= MaxPierce){
            Hide(); // Player disappears after being hit.
        }

        Connect(nameof(Hit), body, "onHit");
        EmitSignal(nameof(Hit), Heal, Effect);

        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }
}
