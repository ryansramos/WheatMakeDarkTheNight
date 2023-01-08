using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightScreen : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _isCompleted;
    public bool isCompleted => _isCompleted;

    void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Reset()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0f);
    }

    public void TurnOn(float duration)
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0f);
        _isCompleted = false;
        StartCoroutine(FadeIn(duration));
    }

    public void TurnOff()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0f);
    }

    IEnumerator FadeIn(float duration)
    {
        Fader fader = SpriteFader.FadeIn(_renderer, duration, this);
        while (!fader.isComplete)
        {
            yield return null;
        }
        _isCompleted = true;
    }
}
