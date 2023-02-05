using System;
using System.ComponentModel;
using Godot;

public abstract class Wave : Node, ITarget
{
    public virtual Node2D TargetNode { get; set; }

    [Signal]
    public delegate void WaveFinished();
    public abstract void ActivateWave(Node2D spawnPoint);

    [Export]
    public bool Enabled { get; set; } = true;
}