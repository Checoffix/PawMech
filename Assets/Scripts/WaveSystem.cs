using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/WavesSystem", fileName = "WavesSystem")]
public class WaveSystem : ScriptableObject
{
    [SerializeField] private WaveEvent[] _waveEvents;

    private static WaveSystem _instance;
    public static WaveSystem I => _instance == null ? LoadWaveSystem() : _instance;

    public WaveEvent[] WaveEvents => _waveEvents;
    private static WaveSystem LoadWaveSystem()
    {
        return _instance = Resources.Load<WaveSystem>("WavesSystem");
    }
}

[Serializable]
public class WaveEvent
{
    public AnimationCurve _ySpawnCurve;
    public AnimationCurve _xSpawnCurve;
    public float time;
    public MovementPattern entryMovement;
    public bool allowToShotFromTheStart;
    public float retreatTime;
    public Vector2 spawnOrigin;
    public float spacing;
    public MovementPattern retreatMovement;
    public SpawnEntry[] enemies;
    internal int GetSummarCount(int i)
    {
        int summarCount = 0;
        foreach (var e in enemies)
        {
            summarCount += e.count;
        }
        return summarCount;
    }
}

[Serializable]
public class SpawnEntry
{
    public GameObject enemyPrefab;
    public int count;
}

