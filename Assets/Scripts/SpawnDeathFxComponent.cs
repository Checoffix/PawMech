using System.Collections.Generic;
using UnityEngine;

public class SpawnDeathFxComponent : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private string _deathParticleName;
    private SpawnComponent _spawnComponent;
    private SpriteRenderer _spriteRenderer;
    private List<Vector2> _positions = new();
    private void Start()
    {
        _spawnComponent = GetComponent<SpawnComponent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChoosePositions();
    }

    private void ChoosePositions()
    {
        for (int i = 0; i < _count; i++)
        {
            _positions.Add(new Vector2(Random.Range(-_spriteRenderer.size.x / 2, _spriteRenderer.size.x / 2), Random.Range(-_spriteRenderer.size.y / 2, _spriteRenderer.size.y / 2)));
        }
    }

    public void SpawnDeathParticles()
    {
        foreach (var position in _positions)
        {
            _spawnComponent.Spawn(_deathParticleName, position);
        }
    }
}
