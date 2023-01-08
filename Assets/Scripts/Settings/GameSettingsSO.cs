using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private float _maxWheatHeight;
    public float maxWheatHeight => _maxWheatHeight;

    [SerializeField]
    private float _highlightWidth;
    public float highlightWidth => _highlightWidth;

    [SerializeField]
    private float _highlightGlowRate;
    public float highlightGlowRate => _highlightGlowRate;

    [SerializeField]
    private float _wheatTotal;
    public float wheatTotal => _wheatTotal;

    [SerializeField]
    private float _maxWheat;
    public float maxWheat => _maxWheat;

    [SerializeField]
    private float _maxStamina;
    public float maxStamina => _maxStamina;

    [SerializeField]
    private float _staminaPerHit;
    public float staminaPerHit => _staminaPerHit;

    [SerializeField]
    private float _maxStaminaRefund;
    public float maxStaminaRefund => _maxStaminaRefund;

    [SerializeField]
    private float _refundThreshold;
    public float refundThreshold => _refundThreshold;
}
