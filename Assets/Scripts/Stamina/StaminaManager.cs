using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private VoidEventChannelSO _onWheatCutEvent;

    [SerializeField]
    private GameObject _meterMask;

    [SerializeField]
    private float _maskMaxPosition;

    [SerializeField]
    private float _maskChangeSpeed;

    private float _currentStamina;
    private Transform _maskTransform;

    public bool isDepleted => _currentStamina < _settings.staminaPerHit;

    void Awake()
    {
        _maskTransform = _meterMask.gameObject.GetComponent<Transform>();
    }
    void OnEnable()
    {
        _onWheatCutEvent.OnEventRaised += OnWheatCut;
    }

    public void OnGameReset()
    {
        _currentStamina = _settings.maxStamina;
        UpdateMeter();
    }

    public void RefundStamina(float percentage)
    {
        if (percentage < _settings.refundThreshold)
        {
            return;
        }
        _currentStamina += percentage * _settings.maxStaminaRefund;
        _currentStamina = Mathf.Min(_currentStamina, _settings.maxStamina);
        UpdateMeter();
    }

    void OnWheatCut()
    {
        if (isDepleted)
        {
            return;
        }
        _currentStamina -= _settings.staminaPerHit;
        _currentStamina = Mathf.Max(0f, _currentStamina);
        UpdateMeter();
    }

    void UpdateMeter()
    {
        float staminaRatio = _currentStamina / _settings.maxStamina;
        Debug.Log(staminaRatio);
        float newPosition = staminaRatio * _maskMaxPosition;
        _maskTransform.localPosition = new Vector3(_maskTransform.localPosition.x, newPosition, 0f);
    }    
}
