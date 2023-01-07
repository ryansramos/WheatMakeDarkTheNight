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
    private int[] _heightArray;

    public float GetHeight(int index)
    {
        float output = 0f;
        if (index < _heightArray.Length && index > -1)
        {
            output = 1f - (float)_heightArray[index] / 80f;
        } 
        return output;  
    }

    public float MaxWheat()
    {
        float spaceRatio = 0f;
        foreach (int height in _heightArray)
        {
            spaceRatio += (float)height / 80;
        }
        spaceRatio /= _heightArray.Length;
        return spaceRatio * _settings.wheatScalar;
    }
}
