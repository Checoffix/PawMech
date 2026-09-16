using UnityEngine;
using UnityEngine.Events;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private string _spawnPrefabName;
    [SerializeField] private string _gunAnimationName;
    [SerializeField] private string _shotgunAnimationName;
    [SerializeField] private int _shotgunBulletCount;
    [Tooltip("ћаксимальный угол наклона у разброса относительно обычного полЄта вправо")]
    [Range(0f, 90f)]
    [SerializeField] private float _spread;
    [SerializeField] private UnityEvent _onStandartShoot;
    [SerializeField] private UnityEvent _onShotgunShoot;
    [SerializeField] private UnityEvent _onLaserShoot;
    private SpriteAnimation _gunAnimation;
    private Cooldown _cooldown;
    private SpawnComponent _spawnComponent;
    private bool _standartShooting = true;
    private bool _shotgunShooting = false;
    private GameObject _laser;

    private void Awake()
    {
        _cooldown = GetComponent<Cooldown>();
        _spawnComponent = GetComponent<SpawnComponent>();
        _gunAnimation = GetComponent<SpriteAnimation>();
        _cooldown.Reset();
    }
    private void FixedUpdate()
    {
        if (_standartShooting)
        {
            if (_cooldown.IsReady)
            {
                _onStandartShoot?.Invoke();
                _gunAnimation.SetClip(_gunAnimationName);
                _spawnComponent.Spawn(_spawnPrefabName, transform.rotation);
                _cooldown.Reset();
            }
        }
        else if (_shotgunShooting)
        {
            if (_cooldown.IsReady)
            {
                _onShotgunShoot?.Invoke();
                _gunAnimation.SetClip(_shotgunAnimationName);
                for (int i = 0; i < _shotgunBulletCount - 1; i++)
                {
                    float rnd = Random.Range(-(_spread / 2), _spread / 2);
                    _spawnComponent.Spawn(_spawnPrefabName, Quaternion.Euler(0, 0, rnd));
                }
                _cooldown.Reset();
            }
        }
    }

    public bool ShotgunShot()
    {
        if (_standartShooting)
        {
            _standartShooting = false;
            _shotgunShooting = true;
            return true;
        }
        return false;
    }

    public bool LaserShot(GameObject gameObject)
    {
        if (_standartShooting)
        {
            _standartShooting = false;
            _laser = _spawnComponent.Spawn(gameObject);
            _onLaserShoot?.Invoke();
            return true;
        }
        return false;
    }

    public void DefaultShooting()
    {
        if (_laser) _laser.GetComponent<DestroyComponent>().Destroy();
        else _shotgunShooting = false;
        _standartShooting = true;
    }
}
