using Godot;
using System;

public class BaseTower : Node2D
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

	private Node2D target;

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
		
	}

	protected virtual void Searchtarget(){

	}
}
