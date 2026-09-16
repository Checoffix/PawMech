using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHintWindow : MonoBehaviour
{
    public void OnYesButton()
    {
        GameSettings.I._firstGame.Value = 0;
        SceneManager.LoadScene(1);
    }
    public void OnNoButton()
    {
        GameSettings.I._firstGame.Value = 0;
        SceneManager.LoadScene(2);
    }
}
