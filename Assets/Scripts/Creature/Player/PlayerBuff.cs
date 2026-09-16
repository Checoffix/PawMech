using System;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    [SerializeField] private Cooldown _attackSpeed;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ShootingComponent _playerShooting;
    [SerializeField] private GameObject _laserProjectile;
    [SerializeField] private float _newAttackSpeedCoefficient;
    [SerializeField] private float _newSizeCoefficient;
    [SerializeField] private float _newSpeedCoefficient;
    private Vector3 _oldSize;
    private bool _isShotgun, _isLaser, _isBuffed;

    private void Awake()
    {
        _oldSize = transform.localScale;
    }
    public void ApplyBuff()
    {
        _isBuffed = true;
        _attackSpeed.ChangeCooldown(_newAttackSpeedCoefficient);
        transform.localScale /= _newSizeCoefficient;
        _playerMovement.ChangeSpeed(_newSpeedCoefficient);
    }
    public void RemoveBuff()
    {
        _isBuffed = false;
        _attackSpeed.DefaultCooldown();
        transform.localScale = _oldSize;
        _playerMovement.DefaultSpeed();
    }
    public bool ShotgunShots()
    {
        return _isShotgun = _playerShooting.ShotgunShot();
    }

    public bool LaserShots()
    {
        return _isLaser = _playerShooting.LaserShot(_laserProjectile);
    }
    public void DefaultShooting()
    {
        _isShotgun = false;
        _isLaser = false;
        _playerShooting.DefaultShooting();
    }

    public (bool, bool, bool) GetAllStatus()
    {
        return (_isShotgun, _isLaser, _isBuffed);
    }
}