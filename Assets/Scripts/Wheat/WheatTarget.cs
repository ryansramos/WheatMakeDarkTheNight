using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatTarget : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;
    private Transform _transform;
    private float _targetWheat;
    public float targetWheat => _targetWheat;

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }
    public void SetTarget(float amount)
    {
        _targetWheat = amount * _settings.expectedEfficiency;
        float wheatRatio = _targetWheat / _settings.maxWheat;
        float newPosition = 20f * wheatRatio + -10f;
        _transform.localPosition = new Vector3(newPosition, _transform.localPosition.y, 0f);
    }
}
