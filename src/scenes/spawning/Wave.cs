using Godot;

public abstract class Wave : Node, ITarget
{
    public virtual NodePath Target { get; set; }

    [Signal]
    public delegate void WaveFinished();
    public abstract void ActivateWave(Node2D spawnPoint);
}