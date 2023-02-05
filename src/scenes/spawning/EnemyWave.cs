using Godot;
using Godot.Collections;

public class EnemyWave : Wave
{

    private State state = State.StartTimeout;

    [Export]
    public PackedScene EnemyScene { get; set; }

    [Export]
    public int MaxHealth { get; set; }

    [Export]
    public int Count { get; set; }

    private int alreadySpawned = 0;

    [Export]
    public float RandomVariationInSeconds { get; set; } = -1;

    [Export]
    public float StartTimeoutInSeconds { get; set; }

    [Export]
    public float DurationInSeconds { get; set; }

    [Export]
    public float CoolDownInSeconds { get; set; }

    private Timer timer = new Timer();

    public override void _Ready()
    {
        base._Ready();
        AddChild(timer);
        if (RandomVariationInSeconds < 0)
        {
            // if RandomVariationInSeconds is not set, use 70% of the secondsPerEnemy as the random variation
            RandomVariationInSeconds = (DurationInSeconds / Count) * 0.7f;
        }
    }

    private void step(Node2D spawnPoint)
    {
        switch (state)
        {
            case State.StartTimeout:
                state = State.Spawn;
                break;
            case State.Spawn:
                alreadySpawned++;
                if (alreadySpawned >= Count) state = State.CoolDown;

                // spawn enemy after a random timeout to make the waves look more natural
                float randomWaitTime = (float)GD.RandRange(0, RandomVariationInSeconds);
                GetTree().CreateTimer(randomWaitTime).Connect("timeout", this, nameof(spawn), new Array { spawnPoint });
                GD.Print($"Random wait time: {randomWaitTime}");

                // set waitTime to either secondsPerEnemy or CoolDownInSeconds depending on whether we have spawned all enemies
                float secondsPerEnemy = DurationInSeconds / Count;
                timer.WaitTime = alreadySpawned < Count ? secondsPerEnemy : CoolDownInSeconds;
                timer.Start();
                break;
            case State.CoolDown:
                GD.Print("Cool down period finished");
                timer.Stop();
                EmitSignal(nameof(WaveFinished));
                break;
        }
    }

    private void spawn(Node2D spawnPoint)
    {
        BaseEnemy enemy = EnemyScene.Instance<BaseEnemy>();
        enemy.TargetNode = TargetNode;
        enemy.MaxHealth = MaxHealth;
        enemy.Health = 1;
        spawnPoint.AddChild(enemy);
        GD.Print($"Spawned enemy {EnemyScene.ResourceName} with MaxHealth {MaxHealth} ({alreadySpawned}/{Count})");
    }

    public override void ActivateWave(Node2D spawnPoint)
    {
        timer.OneShot = false;
        timer.Connect("timeout", this, nameof(step), new Array { spawnPoint });
        timer.WaitTime = StartTimeoutInSeconds;
        timer.Start();
    }


    private enum State
    {
        StartTimeout,
        Spawn,
        CoolDown
    }
}
