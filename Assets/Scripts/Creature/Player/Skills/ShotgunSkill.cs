using System;
using System.Collections;
using UnityEngine;

public class ShotgunSkil : BaseSkill
{
    [SerializeField] private float _skillDuration;
    private Coroutine _shotgunCoroutine;

    [ContextMenu("Use")]
    public override bool Use()
    {
        var players = FindObjectsByType<PlayerBuff>(FindObjectsSortMode.None);
        var _succesfullCast = true;
        foreach (PlayerBuff player in players)
        {
            _succesfullCast = player.ShotgunShots();
        }
        if (_succesfullCast)
        {
            if (_shotgunCoroutine != null) StopCoroutine(_shotgunCoroutine);
            _shotgunCoroutine = StartCoroutine(ShotgunTimer());
            return true;
        }
        else return false;
    }

    private IEnumerator ShotgunTimer()
    {
        yield return new WaitForSeconds(_skillDuration);
        var players = FindObjectsByType<PlayerBuff>(FindObjectsSortMode.None);
        foreach (PlayerBuff player in players)
        {
            player.DefaultShooting();
        }
    }
}
