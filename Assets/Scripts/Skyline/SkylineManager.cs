using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineManager : MonoBehaviour
{
    [SerializeField]
    private SkylineData[] _skylines;
    private SkylineData _activeSkyline = default;
    public SkylineData activeSkyline => _activeSkyline;

    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void LoadSkyline(int index)
    {
        if (index < _skylines.Length && index > -1)
        {
            _activeSkyline = _skylines[index];
            SetSprite(_activeSkyline.sprite);
        }
    }

    public float GetDailyTarget(int index)
    {
        float output = 0f;
        for (int i = 0; i < index + 1; i++)
        {
            output += _skylines[i].MaxWheat();
            if (i >= _skylines.Length)
                break;
        }
        return output;
    }

    void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }
}
