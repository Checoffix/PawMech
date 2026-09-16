using System.Collections;
using UnityEngine;

public class ClusterEnemy : BaseEnemy
{
    public override void Shoot(Vector3 position)
    {
        StartCoroutine(ShootBurst());
    }

    protected virtual IEnumerator ShootBurst()
    {
        SpawnProjectile();
        OnShoot?.Invoke();
        yield return null;
    }
}
