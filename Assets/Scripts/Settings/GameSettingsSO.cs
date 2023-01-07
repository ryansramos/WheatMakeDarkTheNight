using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    public float maxWheatHeight;

    [SerializeField]
    public float highlightWidth;

    [SerializeField]
    public float highlightGlowRate;
}
