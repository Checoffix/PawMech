using System;
using System.Collections;
using UnityEngine;

public class BuffsSkill : BaseSkill
{
    [SerializeField] private float _skillDuration;
    private Coroutine _buffCoroutine;
    [ContextMenu("Use")]
    public override bool Use()
    {
        var players = FindObjectsByType<PlayerBuff>(FindObjectsSortMode.None);
        foreach (PlayerBuff player in players)
        {
            player.ApplyBuff();
        }
        if (_buffCoroutine != null) StopCoroutine(_buffCoroutine);
        _buffCoroutine = StartCoroutine(BuffsTimer());
        return true;
    }

    private IEnumerator BuffsTimer()
    {
        yield return new WaitForSeconds(_skillDuration);
        var players = FindObjectsByType<PlayerBuff>(FindObjectsSortMode.None);
        foreach (PlayerBuff player in players)
        {
            player.RemoveBuff();
        }
    }

}
