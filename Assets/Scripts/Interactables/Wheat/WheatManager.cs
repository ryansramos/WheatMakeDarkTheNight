using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatManager : MonoBehaviour
{
    [SerializeField]
    private WheatStalk[] _stalks;
    private WheatStalk _activeStalk = default;
    [SerializeField]
    private WheatMeter _meter;

    [SerializeField]
    private VoidEventChannelSO _onWheatCutEvent;

    void Start()
    {
        foreach (WheatStalk stalk in _stalks)
        {
            stalk.RequestSetActive.AddListener(OnRequestSetActive);
            stalk.RequestDeactivate.AddListener(OnRequestDeactivate);
            stalk.OnWheatCut.AddListener(OnWheatCut);
        }
        Reset();
    }

    public void Reset()
    {
        foreach (WheatStalk stalk in _stalks)
        {
            stalk.OnReset();
        }
        _meter.Reset();
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

    void OnWheatCut(float amount)
    {
        _onWheatCutEvent.RaiseEvent();
        _meter.Add(amount);
    }
}
