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
    private StaminaManager _stamina;

    [SerializeField]
    private Vector3EventChannel _onClickEvent;

    [SerializeField]
    private VoidEventChannelSO _onWheatCutEvent;

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
        foreach (WheatStalk stalk in _stalks)
        {
            stalk.RequestSetActive.AddListener(OnRequestSetActive);
            stalk.RequestDeactivate.AddListener(OnRequestDeactivate);
            stalk.OnWheatCut.AddListener(OnWheatCut);
        }
        Reset();
    }

    void OnClick(Vector3 position)
    {
        if (_stamina.isDepleted)
        {
            return;
        }
        if (_activeStalk != null)
        {
            _activeStalk.OnClick(position);
        }
    }

    public void Reset()
    {
        foreach (WheatStalk stalk in _stalks)
        {
            stalk.OnReset();
        }
    }

    public void GameReset()
    {
        Reset();
        _meter.Reset();
    }

    public void Pause()
    {
        _activeStalk = null;
        foreach (WheatStalk stalk in _stalks)
        {
            stalk.SetActive(false);
            stalk.Pause();
        }
    }

    public void Resume()
    {
        foreach (WheatStalk stalk in _stalks)
        {
            stalk.Resume();
        }
    }

    public float[] GetStalkHeights()
    {
        float[] heights = new float[_stalks.Length];
        for (int i = 0; i < heights.Length; i++)
        {
            heights[i] = _stalks[i].currentHeight;
        }
        return heights;
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
