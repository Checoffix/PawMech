using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private float _invincibleDuration;
    [SerializeField] private bool _isHavingInvincibleAfterDamage;
    [SerializeField] private UnityEvent _onHeal;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDied;
    private bool _isInvincible = false;
    private float _invincibleStartTime;
    public void ChangeHp(int _change)
    {
        if (!_isInvincible)
        {
            if (_change > 0) _onHeal?.Invoke();
            else
            {
                OnDamage();
                if (_isHavingInvincibleAfterDamage) StartInvincibleFrames();
            }
            _hp += _change;
            if (_hp <= 0) _onDied?.Invoke();
        }
    }

    protected virtual void OnDamage()
    {
        _onDamage?.Invoke();
    }

    public void StartInvincibleFrames()
    {
        _invincibleStartTime = Time.time;
        _isInvincible = true;
    }
    public void StartInvincibleFrames(float value)
    {
        _invincibleStartTime = Time.time;
        _invincibleDuration = value;
        _isInvincible = true;
    }

    private void FixedUpdate()
    {
        if (_isInvincible)
        {
            if (Time.time - _invincibleStartTime >= _invincibleDuration)
            {
                _isInvincible = false;
            }
        }
    }
}
