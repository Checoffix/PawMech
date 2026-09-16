using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameWindow : MonoBehaviour
{
    private GameSession _gameSession;
    private void Start()
    {
        _gameSession = FindFirstObjectByType<GameSession>();
    }

    public void OnExitButton()
    {
        _gameSession.ClearScore();
        SceneManager.LoadScene(0);
    }
}
