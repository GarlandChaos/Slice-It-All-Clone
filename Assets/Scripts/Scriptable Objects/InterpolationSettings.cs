using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InterpolationSettings", menuName = "Settings/Interpolation Settings")]
public class InterpolationSettings : ScriptableObject
{
    [SerializeField]
    AnimationCurve translationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField]
    float translationTime = 1f;

    public AnimationCurve TranslationCurve { get => translationCurve; }
    public float TranslationTime { get => translationTime; }
}