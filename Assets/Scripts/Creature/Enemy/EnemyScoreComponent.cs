using UnityEngine;

public class EnemyScoreComponent : MonoBehaviour
{
    public void GivePoints(int value)
    {
        FindAnyObjectByType<GameSession>().AddScore(value);
    }
}
