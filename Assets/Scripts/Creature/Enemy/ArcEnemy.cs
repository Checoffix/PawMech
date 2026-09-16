using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ArcEnemy : BaseEnemy
{
    [Range(0f, 180f)]
    [SerializeField] private float _arcWide;
    //[SerializeField] private float _shiftPerBullet;
    private float _arcStep;
    private float _arcShift;
    private float _startShift;
    private Vector2 _targetDir, _lookDir;

    protected override void Start()
    {
        base.Start();
        _arcStep = _arcWide / BulletCount;
        _arcShift = -_arcWide / 2 + _arcStep / 2;
        _lookDir = Vector2.left;
    }


    public override void Shoot(Vector3 position)
    {
        ShootArc(position);
    }

    private void ShootArc(Vector3 position)
    {
        _targetDir = position - transform.position;
        float angle = Vector2.SignedAngle(_targetDir, _lookDir);
        for (int i = 0; i < BulletCount; i++)
        {
            float currentAngle = angle + _arcShift + _arcStep * i > _arcShift ? _arcShift + _arcStep * i : angle + _arcShift + _arcStep * i;
            SpawnProjectile(Quaternion.Euler(0, 0, angle + _arcShift + _arcStep * i));
        }
        OnShoot?.Invoke();
    }
}
