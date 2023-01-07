using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;

    [SerializeField]
    private StringEventChannelSO _loadRequestChannel;

    public void OnStartButtonPressed()
    {
        _loadRequestChannel.RaiseEvent(_sceneToLoad);
    }
}
