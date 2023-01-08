using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeWheatManager : MonoBehaviour
{
    [SerializeField]
    private WheatStalk stalk;
    private WheatStalk _activeStalk = default;

    [SerializeField]
    private Vector3EventChannel _onClickEvent;

    void OnEnable()
    {
        _onClickEvent.OnEventRaised += OnClick;
    }

    void OnDisable()
    {
        _onClickEvent.OnEventRaised -= OnClick;
    }

    void Start()
    {

        stalk.RequestSetActive.AddListener(OnRequestSetActive);
        stalk.RequestDeactivate.AddListener(OnRequestDeactivate);
        Reset();
    }

    void OnClick(Vector3 position)
    {
        if (_activeStalk != null)
        {
            _activeStalk.OnClick(position);
        }
    }

    public void Reset()
    {
        stalk.OnReset();
    }

    void OnRequestSetActive(WheatStalk stalk)
    {
        if (_activeStalk != null)
        {
            return;
        }
        _activeStalk = stalk;
        stalk.SetActive(true);
    }

    void OnRequestDeactivate(WheatStalk stalk)
    {
        if (stalk == _activeStalk)
        {
            _activeStalk = null;
        }
        stalk.SetActive(false);
    }
}
