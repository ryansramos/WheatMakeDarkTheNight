using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skyline")]
public class SkylineData : ScriptableObject
{
    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite => _sprite;

    [SerializeField]
    private float _percentShadedArea;
    public float percentShadedArea => _percentShadedArea;

    public float MaxWheat()
    {
        float percentFree = 1 - _percentShadedArea;
        return percentFree * _settings.wheatTotal;
    }
}
