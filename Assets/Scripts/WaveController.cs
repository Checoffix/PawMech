using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpawnComponent))]
public class WaveController : MonoBehaviour
{
    [SerializeField] private bool _isRandom;
    [SerializeField] private float _randomDuration;
    [SerializeField] private UnityEvent _onWavesEndEvent;
    private SpawnComponent _spawnComponent;
    private int _summarCount;
    private int _posIndex;
    private float _step;
    private float _xCord;
    private float _yCord;
    private float _currentTime = 0f;
    private EnemyMovement _enemyMovement;
    private List<Vector2> _spawnPoss = new();
    private bool _stopSpawn = false;
    private void Start()
    {
        StartCoroutine(SpawnWaves());
        _spawnComponent = GetComponent<SpawnComponent>();
    }

    private IEnumerator SpawnWaves()
    {
        if (!_isRandom)
        {
            for (int i = 0; i < WaveSystem.I.WaveEvents.Length; i++)
            {
                yield return new WaitForSeconds(WaveSystem.I.WaveEvents[i].time - _currentTime);
                _currentTime = WaveSystem.I.WaveEvents[i].time;
                PlayWave(i);
            }
            StartCoroutine(CheckForEnemys());
        }
        else
        {
            StartCoroutine(Timer(_randomDuration));
            while (!_stopSpawn)
            {
                yield return new WaitForSeconds(Random.Range(0f, 2f));
                PlayWave(Random.Range(0, WaveSystem.I.WaveEvents.Length));
            }
        }
    }

    private IEnumerator CheckForEnemys()
    {
        var enemies = FindAnyObjectByType<BaseEnemy>();
        while (enemies != null)
        {
            yield return new WaitForSeconds(1f);
            enemies = FindAnyObjectByType<BaseEnemy>();
        }
        yield return new WaitForSeconds(4f);
        _onWavesEndEvent?.Invoke();
    }

    private IEnumerator Timer(float randomDuration)
    {
        yield return new WaitForSeconds(randomDuration);
        _stopSpawn = true;
        StartCoroutine(CheckForEnemys());
    }

    private void PlayWave(int i)
    {
        _summarCount = WaveSystem.I.WaveEvents[i].GetSummarCount(i);

        if (_summarCount <= 1) _step = 0;
        else _step = 1f / (_summarCount - 1);
        for (int j = 0; j < _summarCount; j++)
        {
            _xCord = WaveSystem.I.WaveEvents[i].spawnOrigin.x + WaveSystem.I.WaveEvents[i]._xSpawnCurve.Evaluate(_step * j) * WaveSystem.I.WaveEvents[i].spacing;
            _yCord = WaveSystem.I.WaveEvents[i].spawnOrigin.y + WaveSystem.I.WaveEvents[i]._ySpawnCurve.Evaluate(_step * j) * WaveSystem.I.WaveEvents[i].spacing;
            _spawnPoss.Add(new Vector2(_xCord, _yCord));
        }
        foreach (SpawnEntry spawn in WaveSystem.I.WaveEvents[i].enemies)
        {
            for (int j = 0; j < spawn.count; j++)
            {
                _posIndex = Random.Range(0, _spawnPoss.Count);
                _enemyMovement = _spawnComponent.Spawn(spawn.enemyPrefab, _spawnPoss[_posIndex]).GetComponent<EnemyMovement>();
                _enemyMovement.SetEntry(WaveSystem.I.WaveEvents[i].entryMovement);
                _enemyMovement.SetRetreat(WaveSystem.I.WaveEvents[i].retreatMovement, WaveSystem.I.WaveEvents[i].retreatTime);
                if (WaveSystem.I.WaveEvents[i].allowToShotFromTheStart) _enemyMovement.EnableShoot();
                _spawnPoss.RemoveAt(_posIndex);
            }
        }
    } 
}
