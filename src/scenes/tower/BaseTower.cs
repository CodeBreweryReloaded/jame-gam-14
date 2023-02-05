using Godot;
using System;
using System.Collections.Generic;

public abstract class BaseTower : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    protected int Range = 10;
    [Export]
    protected int AttackSpeed = 4;
    [Export]
    public float ProjectileSpeed = 50.0f;
    [Export]
    protected float LevelFactor = 1.5F;
    [Export]
    protected int Level = 1;
    [Export]
    public int Heal = 10;
    [Export]
    public String Effect = "";    
    [Export]
    public float EffectDuration = 0.0f;
    [Export]
    private Texture emblem;

    [Export(PropertyHint.ResourceType, "PackedScene")]
    protected PackedScene projectileScene;

    private HashSet<Node2D> InRangeList = new HashSet<Node2D>();
    [Export(PropertyHint.ResourceType, "NodePath")]
    private NodePath colliderPath;

    private int tick = 0;

    private BaseProjectile projectile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Sprite>("Emblem").Texture = emblem;
        CollisionShape2D shape = new CollisionShape2D();
        CircleShape2D circle = new CircleShape2D();
        circle.Radius = Range;
        shape.Shape = circle;
        GetNode<Node2D>(colliderPath).AddChild(shape);
        Update();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (tick == 0) {
            Shoot();
        }

        tick++;
        tick %= AttackSpeed;
    }

    protected virtual void Shoot()
    {
        Node2D target = SearchTarget();
        if (target != null) {
            BaseProjectile proj = (BaseProjectile)projectileScene.Instance();
            proj.Target = target;
            proj.Tower = this;

            AddChild(proj);
        }

    }

    protected virtual Node2D SearchTarget()
    {
		float distance = int.MaxValue;
		Node2D closestEnemy = null;
        foreach(Node2D Enemy in InRangeList){
			if(Enemy.GlobalPosition.DistanceTo(this.GlobalPosition) < distance){
				distance = Enemy.GlobalPosition.DistanceTo(this.GlobalPosition);
				closestEnemy = Enemy;
			}
		}
        return closestEnemy;
    }

    protected virtual void LevelUp()
    {
        Level++;
        Heal = (int)(Heal * LevelFactor);
    }


    protected virtual void _on_TowerRange_body_entered(Node2D body)
    {
        InRangeList.Add(body);
    }

    protected virtual void _on_TowerRange_body_exited(Node2D body)
    {
		InRangeList.Remove(body);
    }

}
