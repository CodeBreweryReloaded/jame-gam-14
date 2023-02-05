using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

public class CompositeWave : Wave
{
    private List<Wave> waves = new List<Wave>();

    private int nextWave = 0;
    private Wave nextWaveInstance => waves[nextWave];

    private int finishedWaveCounter = 0;

    [Export]
    private bool ActivateSimultaneously { get; set; } = false;


    public override void _Ready()
    {
        waves = GetChildren().OfType<Wave>().Where(wave => wave.Enabled).ToList();
    }

    public override void ActivateWave(Node2D spawnPoint)
    {
        if (nextWave < waves.Count)
        {
            GD.Print($"Activating wave {nextWave} of {waves.Count} ({nextWaveInstance.GetType().Name})");
            nextWaveInstance.Connect(nameof(Wave.EnemySpawned), this, nameof(OnSpawned));
            nextWaveInstance.TargetNode = TargetNode;
            nextWaveInstance.ActivateWave(spawnPoint);
            // only call ActivateWave when the subwave finishes if ActivateSimultaneously is false as otherwise ActivateWave is called below
            if (!ActivateSimultaneously)
                nextWaveInstance.Connect(nameof(WaveFinished), this, nameof(ActivateWave), new Array { spawnPoint });

            nextWaveInstance.Connect(nameof(WaveFinished), this, nameof(waveFinished));

            nextWave++;

            if (ActivateSimultaneously)
                Call(nameof(ActivateWave), spawnPoint);
        }
    }

    private void waveFinished()
    {
        finishedWaveCounter++;
        if (finishedWaveCounter >= waves.Count)
        {
            EmitSignal(nameof(WaveFinished));
        }
    }
}
