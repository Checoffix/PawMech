using System.Collections;
using UnityEngine;

public class BurstEnemy : BaseEnemy
{
    [Range(0f, 90f)]
    [SerializeField] private float _spread;
    [Range(0f, 20f)]
    [SerializeField] private float _burstMicroPause;
    private Vector2 _targetDir, _lookDir;
    protected override void Start()
    {
        base.Start();
        _lookDir = Vector2.left;
    }

    public override void Shoot(Vector3 position)
    {
        StartCoroutine(ShootBurst(position));
    }

    protected virtual IEnumerator ShootBurst(Vector3 position)
    {
        _targetDir = position - transform.position;
        float angle = Vector2.SignedAngle(_targetDir, _lookDir);
        for (int i = 0; i < BulletCount; i++)
        {
            float rnd = Random.Range(-(_spread / 2), _spread / 2);
            float currentAngle = angle + rnd > _spread ? _spread : angle + rnd;
            SpawnProjectile(Quaternion.Euler(0, 0, currentAngle));
            yield return new WaitForSeconds(_burstMicroPause);
        }
        OnShoot?.Invoke();
        yield return null;
    }
}
