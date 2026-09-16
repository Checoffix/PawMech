using UnityEngine;

public class BombSkill : BaseSkill
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private string _bulletTag;
    public override bool Use()
    {
        var enemies = FindObjectsByType<DestroyComponent>(FindObjectsSortMode.None);
        foreach (DestroyComponent enemy in enemies)
        {
            if (_enemyLayer.value == (1 << enemy.gameObject.layer))
            {
                enemy.Destroy();
            }
            else if (enemy.gameObject.CompareTag(_bulletTag))
            {
                enemy.Destroy();
            }
        }
        _onCast?.Invoke();
        return true;
    }
}
