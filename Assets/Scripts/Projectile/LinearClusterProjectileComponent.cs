using UnityEngine;
using UnityEngine.Events;

public class LinearClusterProjectileComponent : LinearProjectile
{
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _deltaSpeed;
    [SerializeField] private string _projectileName;
    [SerializeField] private UnityEvent _onStopEvent;
    private SpawnComponent _spawnComponent;
    private float _changeDegree;

    protected override void Start()
    {
        base.Start();
        _spawnComponent = GetComponent<SpawnComponent>();
        _changeDegree = 360f / _bulletCount;
    }

    public void SetBulletCount(int value)
    {
        _bulletCount = value;
        _changeDegree = 360f / _bulletCount;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Speed -= _deltaSpeed * Time.fixedDeltaTime;
        if (Speed <= 0)
        {
            for (int i = 0; i < _bulletCount; i++)
            {
                _spawnComponent.Spawn(_projectileName, Quaternion.Euler(0, 0, _changeDegree * i));
            }
            _onStopEvent?.Invoke();
        }
    }
}
