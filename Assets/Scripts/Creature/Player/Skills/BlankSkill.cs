using UnityEngine;

public class BlankSkill : BaseSkill
{
    [SerializeField] private string _bulletTag;
    [ContextMenu("Use")]
    public override bool Use()
    {
        var projectiles = GameObject.FindGameObjectsWithTag(_bulletTag);
        foreach (GameObject projectile in projectiles)
        {
            projectile.GetComponent<DestroyComponent>().Destroy();
        }
        return true;
    }
}
