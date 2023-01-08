using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TextBlock
{
    [TextArea(2,5)]
    [SerializeField]
    public string text;
}
