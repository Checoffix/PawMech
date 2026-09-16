using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int BulletCount;
    [SerializeField] private string TargetTag;
    [SerializeField] private string ProjectileName;
    [SerializeField] protected UnityEvent OnShoot;
    private Animator _animator;
    protected SpawnComponent SpawnComponent;
    private Cooldown _cooldown;
    protected GameObject Target;
    private Transform _position;
    private static readonly int _attack = Animator.StringToHash("Attack");
    protected virtual void Start()
    {
        SpawnComponent = GetComponent<SpawnComponent>();
        _cooldown = GetComponent<Cooldown>();
        _animator = GetComponent<Animator>();
        Target = GameObject.FindWithTag(TargetTag);
    }
    public virtual void SetTarget(GameObject target)
    {
        Target = target;
    }
    public virtual void SetPosition(Transform position)
    {
        _position = position;
    }
    private void CheckTarget()
    {
        if (Target != null)
        {
            if (Target.CompareTag(TargetTag))
            {
                Shoot(Target.transform.position);
            }
        }
        else if (_position != null)
        {
            Shoot(_position.position);
        }
    }
    public abstract void Shoot(Vector3 position);

    protected GameObject SpawnProjectile()
    {
        return SpawnComponent.Spawn(ProjectileName);
    }
    protected GameObject SpawnProjectile(Quaternion rotation)
    {
        return SpawnComponent.Spawn(ProjectileName, rotation);
    }
    protected GameObject SpawnProjectile(Vector3 position)
    {
        return SpawnComponent.Spawn(ProjectileName, position);
    }
    protected GameObject SpawnProjectile(GameObject gameObject)
    {
        return SpawnComponent.Spawn(gameObject);
    }

    protected virtual void FixedUpdate()
    {
        if (_cooldown.IsReady)
        {
            _cooldown.Reset();
            if (_animator) _animator.SetTrigger(_attack);
            CheckTarget();
        }
    }
}
