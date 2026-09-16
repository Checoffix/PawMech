using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingBarFill;
    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindAnyObjectByType<GameSession>();
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        _loadingScreen.SetActive(true);
        float progressValue;
        while (!operation.isDone)
        {
            progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBarFill.value = progressValue;

            yield return null;
        }
    }
}
