using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Settings", menuName = "Settings/Scene Settings")]
public class SceneSettings : ScriptableObject
{
    [SerializeField]
    List<string> scenes = new List<string>();

    public List<string> _Scenes { get => scenes; }
}
