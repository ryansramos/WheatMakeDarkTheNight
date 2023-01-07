using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatHighlight : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;
    private SpriteRenderer _renderer;
    private bool _isOn;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Reset()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0f);
        SetSortingOrder(0);
    }

    void SetSortingOrder(int order)
    {
        _renderer.sortingOrder = order;
    }

    public void TurnOn()
    {
        if (!_isOn)
        {
            SetSortingOrder(2);
            _isOn = true;
            StartCoroutine(_TurnOn());
        }
    }

    public void TurnOff()
    {
        if (_isOn)
        {
            SetSortingOrder(0);
            _isOn = false;
            StopAllCoroutines();
            if (_renderer.color.a == 0f)
            {
                return;
            }
            StartCoroutine(_TurnOff());
        }
    }

    public void UpdateSprite(float height)
    {
        _renderer.size = new Vector2(2 + _settings.highlightWidth, height + _settings.highlightWidth);
    }

    IEnumerator _TurnOn()
    {
        float duration = 1f;
        while (_isOn)
        {
            // This is in the while loop for live testing purposes
            if (_settings.highlightGlowRate > 0)
            {
                duration = 1 / _settings.highlightGlowRate;
            }
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0f);
            Fader fader = SpriteFader.FadeIn(_renderer, duration, this);
            while (!fader.isComplete)
            {
                yield return null;
            }

            fader = SpriteFader.FadeOut(_renderer, duration, this);
            while (!fader.isComplete)
            {
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator _TurnOff()
    {
        float duration = 1f;
        // This is in the while loop for live testing purposes
        if (_settings.highlightGlowRate > 0)
        {
            duration = 1 / _settings.highlightGlowRate;
        }

        Fader fader = SpriteFader.FadeOut(_renderer, duration, this);
        while (!fader.isComplete)
        {
            yield return null;
        }
    }
}
