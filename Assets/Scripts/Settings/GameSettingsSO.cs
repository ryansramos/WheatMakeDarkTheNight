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
    private float _wheatScalar;
    public float wheatScalar => _wheatScalar;

    [SerializeField]
    private float _maxWheat;
    public float maxWheat => _maxWheat;
}
