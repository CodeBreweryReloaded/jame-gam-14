using Godot;
using System;

public class Zombie : BaseEnemy
{
    protected NavigationAgent2D agent;
    protected Node2D anchor;

    private float AudioInterval = 4.0f;

    [Export(PropertyHint.ResourceType, "NodePath")]
    protected NodePath agentPath;

    [Export(PropertyHint.ResourceType, "NodePath")]
    protected NodePath anchorPath;

    [Export(PropertyHint.ResourceType, "Double")]
    private double randomDegrees = 70f;
    private EnemyAudioPlayer enemyAudioPlayer;

    private RandomEffectPlayer2D randomEffectPlayer2D;

    public override void _Ready()
    {
        base._Ready();
        agent = GetNode<NavigationAgent2D>(agentPath);
        anchor = GetNode<Node2D>(anchorPath);
        agent.SetTargetLocation(TargetNode.GlobalPosition);

        enemyAudioPlayer = GetNode<EnemyAudioPlayer>("AudioStreamPlayer2D");
        Timer timer = new Timer();
        timer.Autostart = true;
        timer.OneShot = false;
        timer.WaitTime = AudioInterval;
        timer.Connect("timeout", this, nameof(AudioQueue));
        AddChild(timer);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (agent.IsTargetReached()) return;

        Vector2 currentPosition = anchor.GlobalPosition;
        Vector2 nextPathPosition = agent.GetNextLocation();
        Vector2 nextVelocity = (nextPathPosition - currentPosition).Normalized() * Speed;

        double randomRadiants = degreesToRadiant(randomDegrees);
        MoveAndSlide(nextVelocity.Rotated((float)GD.RandRange(-randomRadiants, randomRadiants)));
    }

    private double degreesToRadiant(double degrees) {
        return degrees * Math.PI / 180;
    }


    private void AudioQueue()
    {
        enemyAudioPlayer.PlayIdle();
    }
}
