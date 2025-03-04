using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadarWaveController : MonoBehaviour
{
    public ParticleSystem radarWaves;
    public float waveInterval = 3.0f; // Time before next wave set
    public float moveSpeed = 5.0f; // Speed at which rings move upwards
    public float maxHeight = 50.0f; // Max height the rings should reach

    void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            for (int i = 0; i < 3; i++)
            {
                emitParams.startSize = 1.0f + (i * 0.5f); // Makes each ring slightly bigger
                emitParams.velocity = new Vector3(0, moveSpeed, 0); // Rings move upwards
                radarWaves.Emit(emitParams, 1);
                yield return new WaitForSeconds(0.2f); // Small delay between each ring
            }

            yield return new WaitForSeconds(waveInterval); // Pause before next set
        }
    }
}
