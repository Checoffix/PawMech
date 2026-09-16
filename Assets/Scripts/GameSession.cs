using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int _hpCount;
    [SerializeField] private int _score;
    [SerializeField] private int _maxScore;
    private TextMeshProUGUI _hpCountText;
    public int HpCount => _hpCount;
    public int Score => _maxScore;

    private void Awake()
    {
        if (GameSessionExist())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            UpdateData();
        }
    }

    private bool GameSessionExist()
    {
        var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
        foreach (var session in sessions)
        {
            if (session != this)
            {
                session.UpdateData();
                return true;
            }
        }
        return false;
    }

    private void UpdateData()
    {
        GameObject go = GameObject.FindGameObjectWithTag("hpText");
        if (go)
        {
            _hpCountText = go.GetComponent<TextMeshProUGUI>();
            _hpCountText.text = "x" + _hpCount.ToString();
        }
    }

    public void SetHpCount(int v)
    {
        _hpCount = v;
    }

    public void DecreaseHp()
    {
        _hpCount--;
        _hpCountText.text = "x" + _hpCount.ToString();
        if (_hpCount == 0)
        {
            _hpCount = 9;
            ClearScore();
            SceneManager.LoadScene(0);
        }
    }

    public void AddScore(int value)
    {
        _score += value;
    }

    public void ClearScore()
    {
        _maxScore = _score;
        _score = 0;
    }
}
