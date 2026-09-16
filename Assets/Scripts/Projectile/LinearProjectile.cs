using UnityEngine;

public class LinearProjectile : BaseProjectile
{
    private Vector2 _nextPoint = new();
    private float dx, dy;

    protected override void Start()
    {
        base.Start();
        _nextPoint = transform.position;
        dx = Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180);
        dy = Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180);
    }
    protected virtual void FixedUpdate()
    {
        _nextPoint.x += dx * Speed * Direction;
        _nextPoint.y += dy * Speed;
        Rigidbody.MovePosition(_nextPoint);
    }
    public override void SetRotation(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }
}
