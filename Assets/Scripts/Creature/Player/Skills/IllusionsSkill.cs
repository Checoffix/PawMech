using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionsSkill : BaseSkill
{
    [SerializeField] private float _deltaY;
    [SerializeField] private string _spawnName;
    [SerializeField] private PlayerBuff _player;
    [SerializeField] private float _skillDuration;
    private List<GameObject> _illusions = new();
    private SpawnComponent _spawnComponent;
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _spawnComponent = GetComponent<SpawnComponent>();
    }
    [ContextMenu("Use")]
    public override bool Use()
    {
        var status = _player.GetAllStatus();
        _illusions.Add(_spawnComponent.Spawn(_spawnName, new Vector3(0, _deltaY)));
        _illusions.Add(_spawnComponent.Spawn(_spawnName, new Vector3(0, -_deltaY)));
        if (status.Item1)
        {
            foreach (var item in _illusions)
            {
                item.GetComponent<PlayerBuff>().ShotgunShots();
            }
        }
        if (status.Item2)
        {
            foreach (var item in _illusions)
            {
                item.GetComponent<PlayerBuff>().LaserShots();
            }
        }
        if (status.Item3)
        {
            foreach (var item in _illusions)
            {
                item.GetComponent<PlayerBuff>().ApplyBuff();
            }
        }
        if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = StartCoroutine(IllusionsTimer());
        _onCast?.Invoke();
        return true;
    }
    private IEnumerator IllusionsTimer()
    {
        yield return new WaitForSeconds(_skillDuration);
        foreach (var illusion in _illusions)
        {
            illusion.GetComponent<DestroyComponent>().Destroy();
        }
        _illusions.Clear();
    }
}
