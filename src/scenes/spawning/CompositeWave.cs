using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

public class CompositeWave : Wave
{
    private List<Wave> waves = new List<Wave>();

    private int nextWave = 0;
    private Wave nextWaveInstance => waves[nextWave];


    public override void _Ready()
    {
        waves = GetChildren().OfType<Wave>().ToList();
    }

    public override void ActivateWave(Node2D spawnPoint)
    {
        if (nextWave < waves.Count)
        {
            GD.Print($"Activating wave {nextWave} of {waves.Count} ({nextWaveInstance.GetType().Name})");
            nextWaveInstance.Target = Target;
            nextWaveInstance.ActivateWave(spawnPoint);
            nextWaveInstance.Connect(nameof(WaveFinished), this, nameof(ActivateWave), new Array { spawnPoint });
            nextWave++;
        }
        else
        {
            EmitSignal(nameof(WaveFinished));
        }
    }

}
