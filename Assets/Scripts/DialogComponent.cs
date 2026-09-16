using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DialogComponent : MonoBehaviour
{
    [SerializeField] private string[] _text;
    [SerializeField] private float _typingWait;
    [SerializeField] private UnityEvent _typingEvent;
    [SerializeField] private DialogEvent[] _events;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private int _neededSkill;
    [SerializeField] private int _nextScene;
    private int _currentTextIndex;
    private int _currentSymbolIndex;
    private bool _isTyping = false;
    private bool _lockUntilEvent = false;
    private Coroutine _typingCoroutine;
    private GameSession _gameSession;
    private void Start()
    {
        StartTyping();
        _gameSession = FindFirstObjectByType<GameSession>();
    }

    private void StartTyping()
    {
        CheckEvents();
        _isTyping = true;
        _currentSymbolIndex = 0;
        _textField.text = string.Empty;
        _typingCoroutine = StartCoroutine(TypeText());
    }
    public void OnClick()
    {
        if (_isTyping)
        {
            StopCoroutine(_typingCoroutine);
            _isTyping = false;
            _textField.text = _text[_currentTextIndex];
            _currentTextIndex++;
        }
        else
        {
            if (!_lockUntilEvent)
            {
                if (_currentTextIndex == _text.Length)
                {
                    Destroy(_canvas);
                    _gameSession.SetHpCount(9);
                    SceneManager.LoadScene(_nextScene);
                }
                else StartTyping();
            }
        }
    }

    private void CheckEvents()
    {
        foreach (var _event in _events)
        {
            if (_event._index == _currentTextIndex)
            {
                _event._event?.Invoke();
            }
        }
    }

    private IEnumerator TypeText()
    {
        while (_currentSymbolIndex < _text[_currentTextIndex].Length)
        {
            _typingEvent?.Invoke();
            _textField.text += _text[_currentTextIndex][_currentSymbolIndex];
            _currentSymbolIndex++;
            yield return new WaitForSeconds(_typingWait);
        }
        _currentTextIndex++;
        _isTyping = false;
        yield return null;
    }

    public void SetLock(bool value)
    {
        _lockUntilEvent = value;
    }

    public void NextText()
    {
        StartTyping();
    }
    public void CheckSkill(int value)
    {
        if (value == _neededSkill)
        {
            _neededSkill = -10;
            _lockUntilEvent = false;
            StartTyping();
        }
    }
    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    [Serializable]
    private class DialogEvent
    {
        public int _index;
        public UnityEvent _event;
    }
}
