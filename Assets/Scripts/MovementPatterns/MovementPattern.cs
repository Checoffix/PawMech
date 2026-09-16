using UnityEngine;

public abstract class MovementPattern : ScriptableObject
{
    public float _maxDuration;
    [SerializeField] protected float Speed;
    public abstract Vector2 GetVelocity(float t);
}
