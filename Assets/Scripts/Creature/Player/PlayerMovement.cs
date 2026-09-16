using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CreatureMovement
{
    [SerializeField] private float _speed;
    private float _oldSpeed;

    protected override void Awake()
    {
        base.Awake();
        _oldSpeed = _speed;
    }

    public override void SetMovement(Vector2 direction)
    {
        Movement = direction * _speed;
    }

    public void ChangeSpeed(float newSpeedCoefficient)
    {
        _speed *= newSpeedCoefficient;
    }
    public void DefaultSpeed()
    {
        _speed = _oldSpeed;
    }
}
