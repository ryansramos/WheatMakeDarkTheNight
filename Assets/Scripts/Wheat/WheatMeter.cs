using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatMeter : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private float _maskMaxPosition;

    [SerializeField]
    private GameObject _maskObject;
    private Transform _maskTransform;

    private float _currentWheat;
    public float currentWheat => _currentWheat;
    
    void Awake()
    {
        _maskTransform = _maskObject.GetComponent<Transform>();
    }

    public void Reset()
    {
        _maskTransform.localPosition = Vector3.zero;
        _currentWheat = 0f;
    }

    public void Add(float amount)
    {
        float wheatAmount = amount * _settings.wheatTotal / 9;
        _currentWheat += wheatAmount;
        float wheatRatio = wheatAmount / _settings.maxWheat;
        Vector3 newPosition = _maskTransform.localPosition + wheatRatio * _maskMaxPosition * Vector3.right;
        _maskTransform.localPosition = newPosition;
    }
}
