using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineManager : MonoBehaviour
{
    [SerializeField]
    private SkylineData[] _skylines;
    private SkylineData _activeSkyline = default;

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

    void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }
}
