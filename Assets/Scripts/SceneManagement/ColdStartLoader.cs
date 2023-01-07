using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColdStartLoader : MonoBehaviour
{
    [SerializeField]
    private string _persistentSceneName;
#if UNITY_EDITOR
    void OnEnable()
    {
        LoadPersistentManagers();
    }

    void LoadPersistentManagers()
    {
        if (SceneManager.GetSceneByName(_persistentSceneName).isLoaded)
        {   
            return;
        }   
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_persistentSceneName, LoadSceneMode.Additive);
    }
#endif
}
