using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationLoader : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;

    [SerializeField]
    private string _persistentSceneName;

    void Start()
    {
        AsyncOperation pmLoad = SceneManager.LoadSceneAsync(_persistentSceneName, LoadSceneMode.Additive);
        pmLoad.completed += LoadTitle;
    }

    void LoadTitle(AsyncOperation operation) => StartCoroutine(LoadTitleRoutine());
    IEnumerator LoadTitleRoutine()
    {
        AsyncOperation titleLoad = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        while (!titleLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad));
        UnloadInitialization();
    }

    void UnloadInitialization()
    {
        SceneManager.UnloadSceneAsync("Initialization");
    }
}
