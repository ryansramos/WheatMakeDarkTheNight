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
        textObj.transform.localPosition = new Vector3(154.5f, 44.4000015f, 0f);
        TextMeshProUGUI _textMesh = textObj.GetComponent<TextMeshProUGUI>();
        _textMesh.text = text;
    }
}