using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject _tutorialHintWindow;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private GameSession _gameSession;
    private Canvas _canvas;

    private void Start()
    {
        _canvas = FindFirstObjectByType<Canvas>();
        _gameSession = FindFirstObjectByType<GameSession>();
        if (_gameSession != null) _scoreText.text = "Max score: " + _gameSession.Score;
    }
    public void OnStart()
    {
        if (GameSettings.I._firstGame.Value != 0)
        {
            Instantiate(_tutorialHintWindow, _canvas.transform);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void OnSetting()
    {
        Instantiate(_settingMenu, _canvas.transform);
    }

    public void OnExit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
