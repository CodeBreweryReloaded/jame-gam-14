using Godot;
using Godot.Collections;

public class EnemyWave : Wave
{

    [Export]
    public PackedScene EnemyScene { get; set; }

    [Export]
    public int Count { get; set; }

    private int alreadySpawned = 0;

    [Export]
    public float DurationInSeconds { get; set; }

    [Export]
    public float CoolDownInSeconds { get; set; }

    private Timer timer;


    private void spawn(Node2D spawnPoint)
    {
        if (alreadySpawned < Count)
        {
            alreadySpawned++;
            BaseEnemy enemy = EnemyScene.Instance<BaseEnemy>();
            enemy.Target = Target;
            spawnPoint.AddChild(enemy);

            // set waitTime to either secondsPerEnemy or CoolDownInSeconds depending on whether we have spawned all enemies
            float secondsPerEnemy = DurationInSeconds / Count;
            timer.WaitTime = alreadySpawned < Count ? secondsPerEnemy : CoolDownInSeconds;
            timer.Start(); // recursive call to spawn
        }
        else
        {
            EmitSignal(nameof(WaveFinished));
        }
    }

    public override void ActivateWave(Node2D spawnPoint)
    {
        if (timer != null)
        {
            timer.QueueFree();
        }
        timer = new Timer();
        AddChild(timer);
        timer.Connect("timeout", this, nameof(spawn), new Array { spawnPoint });
        spawn(spawnPoint);
    }

    private enum State
    {
        Spawning,
        Cooldown
    }

}
