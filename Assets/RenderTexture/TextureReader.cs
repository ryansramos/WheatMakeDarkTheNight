using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TextureReader : MonoBehaviour
{
    [SerializeField]
    private Camera _renderCam;

    [SerializeField]
    private RenderTexture _rt;

    [SerializeField]
    private float _valueThreshold;

    private Texture2D _texture;
    private bool _isProcessing;
    public bool isProcessing => _isProcessing;
    private float _shadedPercent;
    public float shadedPercent => _shadedPercent;
#if UNITY_EDITOR
    [ContextMenu("Read texture")]
    void ReadTextureFromInspector()
    {
        ReadTexture();
        Debug.Log(_shadedPercent);
    }
#endif
    public async void ReadTexture()
    {
        RenderTexture.active = _rt;
        _texture = new Texture2D(_rt.width, _rt.height, TextureFormat.RGBA32, 1, false);
        _texture.ReadPixels(new Rect(0, 0, _rt.width, _rt.height), 0, 0);
        RenderTexture.active = null;
        Color32[] colorArray = _texture.GetPixels32();
        _isProcessing = true;
        await CalculateShadedArea(colorArray);
        _isProcessing = false;
    }

    Task CalculateShadedArea(Color32[] colors)
    {
        int shadedCount = 0;
        foreach (Color color in colors)
        {
            if (color.a < .1f)
                continue;
            
            Color.RGBToHSV(color, out float h, out float s, out float value);
            if (value < _valueThreshold)
            {
                shadedCount++;
            }
        }
        _shadedPercent = (float)shadedCount / (float)colors.Length;
        return Task.CompletedTask;
    }
}
