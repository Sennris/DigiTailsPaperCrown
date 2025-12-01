using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class SoundManager2 : MonoBehaviour
{
    [SerializeField]
    SoundList2 soundList;

    [SerializeField]
    Dictionary<string, AudioClip> SoundList;
}

[Serializable]
public class SoundList2
{
    [SerializeField]
    SoundListItem[] categories;
}

[Serializable]
public class SoundListItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip[] audioClipList;
}