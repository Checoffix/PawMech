using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : CreatureMovement
{
    [SerializeField] private UnityEvent _afterEntry;
    [SerializeField] private UnityEvent _onRetreat;
    [SerializeField] private UnityEvent _onVisible;
    [SerializeField] private UnityEvent _onInvisible;
    private MovementPattern _entryPattern;
    private float _entryTime;
    private MovementPattern _retreatPattern;
    private float _retreatTime;
    private float _entryStartTime;
    private float _retreatStartTime;
    private bool _justStopped = true;
    private bool _onStartingRetreat = true;

    protected override void Awake()
    {
        base.Awake();
        _entryStartTime = Time.time;
    }
    public void SetEntry(MovementPattern entryPattern)
    {
        _entryPattern = entryPattern;
        _entryTime = _entryPattern._maxDuration;
    }
    public void SetRetreat(MovementPattern retreatPattern, float retreatTime)
    {
        _retreatPattern = retreatPattern;
        _retreatTime = retreatTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (_entryTime >= 0 || _entryTime == -1)
        {
            if (_entryTime >= 0)
            {
                _entryTime -= Time.fixedDeltaTime;
            }
            Movement = _entryPattern.GetVelocity(Time.time - _entryStartTime);
        }
        else if (_retreatTime >= 0)
        {
            Movement = Vector2.zero;
            if (_justStopped)
            {
                _afterEntry?.Invoke();
                _justStopped = false;
            }
            _retreatTime -= Time.fixedDeltaTime;
        }
        else if (_retreatTime != -1)
        {
            if (_onStartingRetreat)
            {
                _onRetreat?.Invoke();
                _retreatStartTime = Time.time;
                _onStartingRetreat = false;
            }
            Movement = _retreatPattern.GetVelocity(Time.time - _retreatStartTime);
        }
        else if (_justStopped)
        {
            _afterEntry?.Invoke();
            _justStopped = false;
            Movement = Vector2.zero;
        }
    }

    private void OnBecameVisible()
    {
        _onVisible?.Invoke();
    }

    private void OnBecameInvisible()
    {
        _onInvisible?.Invoke();
    }

    internal void EnableShoot()
    {
        GetComponent<BaseEnemy>().enabled = true;
    }
}
