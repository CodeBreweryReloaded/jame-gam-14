using Godot;
using System;

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
	protected abstract PackedScene ProjectileScene{get;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	protected virtual void Shoot(){
		Searchtarget();
		BaseProjectile proj = (BaseProjectile)ProjectileScene.Instance();
		proj.Target = Searchtarget();
		proj.Tower = this;

		GetParent().AddChild(proj);

	}

	protected virtual Node2D Searchtarget(){
		Node2D target = new Node2D();

		//TODO

		return null;
	}

	private void LevelUp(){//TODO is level up a tower's responsibility?
		Level++; 
		Heal = (int) (Heal * LevelFactor);
	}

	
}
