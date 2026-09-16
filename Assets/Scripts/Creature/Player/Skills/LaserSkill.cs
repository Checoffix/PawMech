using System.Collections;
using UnityEngine;

public class LaserSkill : BaseSkill
{
    [SerializeField] private float _skillDuration;
    private Coroutine _laserCoroutine;

    [ContextMenu("Use")]
    public override bool Use()
    {
        var players = FindObjectsByType<PlayerBuff>(FindObjectsSortMode.None);
        var _succesfullCast = true;
        foreach (PlayerBuff player in players)
        {
            _succesfullCast = player.LaserShots();
        }
        if (_succesfullCast)
        {
            if (_laserCoroutine != null) StopCoroutine(_laserCoroutine);
            _laserCoroutine = StartCoroutine(LaserTimer());
            return true;
        }
        else return false;
    }

    private IEnumerator LaserTimer()
    {
        yield return new WaitForSeconds(_skillDuration);
        var players = FindObjectsByType<PlayerBuff>(FindObjectsSortMode.None);
        foreach (PlayerBuff player in players)
        {
            player.DefaultShooting();
        }
    }
}
