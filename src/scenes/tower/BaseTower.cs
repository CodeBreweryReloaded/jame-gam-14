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
    protected float AttackSpeed = 10.0F;
    [Export]
    protected float LevelFactor = 1.5F;
    [Export]
    protected int Level = 1;
    [Export]
    public int Heal = 10;
    [Export]
    public String Effect = "";
    protected abstract PackedScene ProjectileScene { get; }
    private HashSet<Node2D> InRangeList = new HashSet<Node2D>();
    [Export(PropertyHint.ResourceType, "NodePath")]
    private NodePath colliderPath;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
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

    protected virtual void Shoot()
    {
        BaseProjectile proj = (BaseProjectile)ProjectileScene.Instance();
        proj.Target = Searchtarget();
        proj.Tower = this;

        GetParent().AddChild(proj);

    }

    protected virtual Node2D Searchtarget()
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
