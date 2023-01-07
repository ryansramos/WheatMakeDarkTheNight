using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteFader
{
    public static Fader FadeIn(SpriteRenderer renderer, float duration, MonoBehaviour caller)
    {
        Fader fader = new Fader(renderer, caller);
        fader.Fade(1f, duration);
        return fader;
    }

    public static Fader FadeOut(SpriteRenderer renderer, float duration, MonoBehaviour caller)
    {
        Fader fader = new Fader(renderer, caller);
        fader.Fade(0f, duration);
        return fader;
    }
}

public class Fader
{   
    private SpriteRenderer _renderer;
    private MonoBehaviour _caller;
    public bool isComplete;

    public Fader(SpriteRenderer renderer, MonoBehaviour caller)
    {
        _renderer = renderer;
        _caller = caller;
        isComplete = false;
    }

    public void Fade(float target, float duration)
    {
        _caller.StartCoroutine(_Fade(target, duration));
    }

    IEnumerator _Fade(float target, float duration)
    {
        target = Mathf.Clamp(target, 0f, 1f);
        if (duration <= 0f)
        {
            isComplete = true;
            yield break;
        }
        Color startColor = _renderer.color;
        float startAlpha = startColor.a;
        float totalFade = target - startAlpha;

        float timer = 0f;
        while (timer < duration)
        {
            float newAlpha = startAlpha + totalFade * (timer / duration);
            UpdateAlpha(startColor, newAlpha);
            timer += Time.deltaTime;
            yield return null;
        }
        UpdateAlpha(startColor, target);
        isComplete = true;
    }

    public bool GetComplete()
    {
        Debug.Log(isComplete);
        return isComplete;
    }

    void UpdateAlpha(Color c, float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0f, 1f);
        Color newColor = new Color(c.r, c.g, c.b, alpha);
        _renderer.color = newColor;
    }

}
