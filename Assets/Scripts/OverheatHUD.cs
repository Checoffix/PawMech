using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OverheatHUD : MonoBehaviour
{
    [SerializeField] private Image _overheatColor;
    [SerializeField] private float _overheatPerTick;
    [SerializeField] private float _overheatDecreasePerTick;
    [SerializeField] private UnityEvent _onOverheat;
    private float _currentOverheat;
    private float _requiredOverheat;
    private float _flashOverheat;
    private Cooldown _flashingTime;
    private bool _isFlashing = false;
    private bool _isDecreasing = false;
    private int i = 0;

    private void Start()
    {
        _flashingTime = GetComponent<Cooldown>();
    }

    public bool IncreaseHeat(int increaseValue)
    {
        if (!_isDecreasing && _requiredOverheat != 1)
        {
            _requiredOverheat += increaseValue / 100f;
            if (_requiredOverheat > 1) _requiredOverheat = 1;
            _isFlashing = false;
            _flashOverheat = 0;
            _overheatColor.fillAmount = _currentOverheat;
            return true;
        }
        else return false;
    }

    public void DecreaseHeat()
    {
        _currentOverheat = 0;
        _isDecreasing = false;
    }
    public void DisableFlashHeat()
    {
        _isFlashing = false;
        _flashOverheat = 0;
        _overheatColor.fillAmount = _currentOverheat;
    }
    public void ClearHeat()
    {
        DecreaseHeat();
        DisableFlashHeat();
    }
    public void FlashHeat(int increaseValue)
    {
        if (!_isDecreasing)
        {
            _flashOverheat = increaseValue / 100f;
            _isFlashing = true;
            _flashingTime.Reset();
        }
    }
    private void OnOverheat()
    {
        _requiredOverheat = 0;
        _isDecreasing = true;
        _onOverheat?.Invoke();
    }

    private void FixedUpdate()
    {
        if (_isFlashing)
        {
            if (_flashingTime.IsReady)
            {
                if (i % 2 == 0) _overheatColor.fillAmount = _currentOverheat;
                else _overheatColor.fillAmount = _currentOverheat + _flashOverheat > 1 ? 1 : _currentOverheat + _flashOverheat;
                i = (int)Mathf.Repeat(i + 1, 2);
                _flashingTime.Reset();
            }
        }
        if (!_isDecreasing)
        {
            if (_currentOverheat < _requiredOverheat)
            {
                _currentOverheat += _overheatPerTick;
                if (_currentOverheat > 1) _currentOverheat = 1;
                _overheatColor.fillAmount = _currentOverheat;
            }
        }
        else if (_currentOverheat > _requiredOverheat)
        {
            _currentOverheat -= _overheatDecreasePerTick;
            if (_currentOverheat < 0)
            {
                _currentOverheat = 0;
                _isDecreasing = false;
            }
            _overheatColor.fillAmount = _currentOverheat;
        }
        if (_currentOverheat == 1)
        {
            OnOverheat();
        }
    }

    public bool IncreaseCurrentFlash()
    {
        return IncreaseHeat((int)(_flashOverheat * 100));
    }
}
