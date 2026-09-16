using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float Speed;

    protected Rigidbody2D Rigidbody;
    protected int Direction;

    protected virtual void Start()
    {
        Direction = transform.lossyScale.x > 0 ? 1 : -1;
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    public virtual void SetTarget(GameObject gameObject)
    {
        return;
    }
    public virtual void SetPosition(Vector3 targetPosition)
    {
        return;
    }
    public virtual void SetRotation(Quaternion quaternion)
    {
        return;
    }
}
