using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class Spawner : CompositeWave
{
    [Export]
    public NodePath Target { get; set; }

    [Export]
    public NodePath SpawnPointPath { get; set; }


    public override void _Ready()
    {
        base._Ready();

        TargetNode = GetNode<Node2D>(Target);

        Node2D spawnPoint = GetNode<Node2D>(SpawnPointPath);
        ActivateWave(spawnPoint);
    }
}
