using System.Collections;
using UnityEngine;

public class LaserEnemy : BaseEnemy
{
    [SerializeField] private float _attackTime;
    [SerializeField] private GameObject _laserPrefab;
    private GameObject _laserBeam;
    private float _startAttackTime;
    private bool _isFiring = false;
    public override void Shoot(Vector3 position)
    {
        StartCoroutine(ShootLaser());
    }

    protected virtual IEnumerator ShootLaser()
    {
        if (!_isFiring)
        {
            _isFiring = true;
            _startAttackTime = Time.time;
            _laserBeam = SpawnProjectile(_laserPrefab);
        }
        yield return null;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_isFiring)
        {
            if (Time.time - _startAttackTime >= _attackTime)
            {
                if (_laserBeam) _laserBeam.GetComponent<DestroyComponent>().Destroy();
                _isFiring = false;
            }
        }
    }
}
