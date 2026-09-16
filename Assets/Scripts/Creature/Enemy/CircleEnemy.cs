using System.Collections;
using UnityEngine;

public class CircleEnemy : BaseEnemy
{
    [SerializeField] private float _rotationSpeed;
    [Range(0f, 20f)]
    [SerializeField] private float _burstMicroPause;

    public override void Shoot(Vector3 position)
    {
        StartCoroutine(ShootCircle());
    }
    private IEnumerator ShootCircle()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SpawnProjectile(Quaternion.Euler(0, 0, transform.eulerAngles.z + 90 * j));
            }
            yield return new WaitForSeconds(_burstMicroPause);
        }
        OnShoot?.Invoke();
        yield return null;
    }
    protected override void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + _rotationSpeed);
        base.FixedUpdate();
    }
}
