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
    private int _maxStamina;
    public int maxStamina => _maxStamina;

    [SerializeField]
    private int _staminaPerHit;
    public int staminaPerHit => _staminaPerHit;


    [Header("Thresholds")]
    [Range(0f, 1f)]
    [SerializeField]
    private float _blockTier1;
    public float blockTier1 => _blockTier1;

    [Range(0f, 1f)]
    [SerializeField]
    private float _blockTier2;
    public float blockTier2 => _blockTier2;

    [Range(0f, 1f)]
    [SerializeField]
    private float _blockTier3;
    public float blockTier3 => _blockTier3;

    
    [Range(0f, 1f)]
    [SerializeField]
    private float _blockTier4;
    public float blockTier4 => _blockTier4;

    
    [Range(0f, 1f)]
    [SerializeField]
    private float _blockTier5;
    public float blockTier5 => _blockTier5;

    [Header("Refunds")]
    [SerializeField]
    [Range(-10, 10)]
    private int _refundTier1;
    public int refundTier1 => _refundTier1;
    [SerializeField]
    [Range(-10, 10)]
    private int _refundTier2;
    public int refundTier2 => _refundTier2;
    [SerializeField]
    [Range(-10, 10)]
    private int _refundTier3;
    public int refundTier3 => _refundTier3;
    [SerializeField]
    [Range(-10, 10)]
    private int _refundTier4;
    public int refundTier4 => _refundTier4;
    [SerializeField]
    [Range(-10, 10)]
    private int _refundTier5;
    public int refundTier5 => _refundTier5;
}
