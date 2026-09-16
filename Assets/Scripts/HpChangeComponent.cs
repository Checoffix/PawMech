using UnityEngine;

public class HpChangeComponent : MonoBehaviour
{
    [SerializeField] private int _hpChange;

    public void ChangeHP(GameObject _gm)
    {
        if (_gm.TryGetComponent<HealthComponent>(out var _health))
        {
            _health.ChangeHp(_hpChange);
        }
    }
}
