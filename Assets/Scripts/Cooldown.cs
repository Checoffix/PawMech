using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private float _value;
    private float _timesUp;
    private float _oldValue;

    private void Start()
    {
        _oldValue = _value;
    }
    public void Reset()
    {
        _timesUp = Time.time + _value;
    }

    public void ChangeCooldown(float newSpeedCoefficient)
    {
        _value /= newSpeedCoefficient;
    }
    public void DefaultCooldown()
    {
        _value = _oldValue;
    }

    public bool IsReady => _timesUp <= Time.time;
}
