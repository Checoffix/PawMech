using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    protected Animator animator;
    protected Vector2 Movement;
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public virtual void SetMovement(Vector2 direction)
    {
        Movement = direction;
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.linearVelocity = Movement;
    }
}
