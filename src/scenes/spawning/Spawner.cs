using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class Spawner : CompositeWave
{

    [Signal]
    public delegate void OnAllEnemiesCured();

    [Export]
    public NodePath Target { get; set; }

    [Export]
    public NodePath SpawnPointPath { get; set; }

    private Node2D spawnPointNode;

    private int enemySpawned = 0;

    public override void _Ready()
    {
        base._Ready();

        AutoPlayWaves = false;

        TargetNode = GetNode<Node2D>(Target);

        spawnPointNode = GetNode<Node2D>(SpawnPointPath);
        Connect(nameof(EnemySpawned), this, nameof(onEnemySpawned));
    }


    public void StartWave()
    {
        ActivateWave(spawnPointNode);
    }


    private void onEnemySpawned(BaseEnemy enemy)
    {
        enemySpawned++;
        enemy.Connect("child_exiting_tree", this, nameof(onEnemyExitingTree));
    }

    private void onEnemyExitingTree(BaseEnemy enemy)
    {
        enemySpawned--;
        if (enemySpawned == 0)
        {
            EmitSignal(nameof(OnAllEnemiesCured));
        }
    }
}
