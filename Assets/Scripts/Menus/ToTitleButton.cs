using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTitleButton : MonoBehaviour
{
    [SerializeField]
    private string _titleSceneName;

    [SerializeField]
    private StringEventChannelSO _loadRequestEvent;

    public void ToTitle()
    {
        _loadRequestEvent.RaiseEvent(_titleSceneName);
    }
}
