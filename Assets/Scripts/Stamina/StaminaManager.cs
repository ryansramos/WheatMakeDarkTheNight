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

    [SerializeField]
    private StaminaTextPlayer _text;

    private int _currentStamina;
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
        int refund = 0;
        string pretext = "+";
        if (percentage > _settings.blockTier1)
        {
            refund = _settings.refundTier1;
        }
        else if (percentage > _settings.blockTier2)
        {
            refund = _settings.refundTier2;
        }
        else if (percentage > _settings.blockTier3)
        {
            refund = _settings.refundTier3;
        }
        else if (percentage > _settings.blockTier4)
        {
            refund = _settings.refundTier4;
        }
        else
        {
            refund = _settings.refundTier5;
        }
        if (refund != 0)
        {
            if (refund < 0)
            {
                pretext = "-";
            }
            string playText = pretext + refund.ToString();
            _text.PlayText(playText);
        }
        _currentStamina += refund;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _settings.maxStamina);
        UpdateMeter();
    }

    void OnWheatCut()
    {
        if (isDepleted)
        {
            return;
        }
        _text.PlayText("-" + _settings.staminaPerHit.ToString());
        _currentStamina -= _settings.staminaPerHit;
        _currentStamina = Mathf.Max(0, _currentStamina);
        UpdateMeter();
    }

    void UpdateMeter()
    {
        float staminaRatio = (float)_currentStamina / (float)_settings.maxStamina;
        float newPosition = staminaRatio * _maskMaxPosition;
        _maskTransform.localPosition = new Vector3(_maskTransform.localPosition.x, newPosition, 0f);
    }    
}
