using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    [SerializeField]
    private TextMeshProUGUI _feedbackText;

    [SerializeField]
    private GameSettingsSO _settings;

    [SerializeField]
    private TextBlock[] _feedback;

    public void OnReset()
    {
        _textMesh.gameObject.SetActive(false);
        _feedbackText.gameObject.SetActive(false);
    }

    public void PlayText(float percent)
    {
        _textMesh.gameObject.SetActive(true);
        percent *= 100f;
        _textMesh.text = percent.ToString("F1") + " % of light pollution blocked.";
    }

    public void StopText()
    {
        _textMesh.gameObject.SetActive(false);
    }

    public void PlayFeedbackText(float percent)
    {
        _feedbackText.gameObject.SetActive(true);
        Debug.Log(percent);
        if (percent > _settings.blockTier1)
        {
            SetFeedbackText(0);
        }
        else if (percent > _settings.blockTier2)
        {
            SetFeedbackText(1);
        }
        else if (percent > _settings.blockTier3)
        {
            SetFeedbackText(2);
        }
        else if (percent > _settings.blockTier4)
        {
            SetFeedbackText(3);
        }
        else
        {
            SetFeedbackText(4);
        }
    }

    public void StopFeedbackText()
    {
        _feedbackText.gameObject.SetActive(false);
    }

    void SetFeedbackText(int tier)
    {
        _feedbackText.text = _feedback[tier].text;
    }
}
