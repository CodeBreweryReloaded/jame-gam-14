using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class Spawner : CompositeWave
{

    [Signal]
    public delegate void OnAllEnemiesCured();

    [Signal]
    public delegate void GameWon();

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
        Connect(nameof(WaveFinished), this, nameof(onWaveFinished));
    }


    public void StartWave()
    {
        ActivateWave(spawnPointNode);
    }


    private void onEnemySpawned(BaseEnemy enemy)
    {
        enemySpawned++;
        enemy.Connect("tree_exiting", this, nameof(onEnemyExitingTree));
        GD.Print($"Enemy spawned, {enemySpawned} enemies left");
    }

    private void onEnemyExitingTree()
    {
        enemySpawned--;
        GD.Print($"Enemy exited tree, {enemySpawned} enemies left");
        onWaveFinished();
    }

    private void onWaveFinished()
    {
        if (enemySpawned <= 0)
        {
            EmitSignal(nameof(OnAllEnemiesCured));
            if (FinishedWaveCounter >= WaveCount)
            {
                EmitSignal(nameof(GameWon));
            }
        }
    }

}
