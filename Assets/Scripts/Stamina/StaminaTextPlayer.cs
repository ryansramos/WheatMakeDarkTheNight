using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaminaTextPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _textPrefab;

    [SerializeField]
    private RectTransform _canvas;

    public void PlayText(string text)
    {
        GameObject textObj = Instantiate(_textPrefab, _canvas.transform);
        TextMeshProUGUI _textMesh = textObj.GetComponent<TextMeshProUGUI>();
        _textMesh.text = text;
    }
}