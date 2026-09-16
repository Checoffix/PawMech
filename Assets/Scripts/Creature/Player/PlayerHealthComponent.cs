using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindAnyObjectByType<GameSession>();
    }
    protected override void OnDamage()
    {
        base.OnDamage();
        _gameSession.DecreaseHp();
    }
}
