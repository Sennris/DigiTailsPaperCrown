using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SoundManager2 : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField]
    SoundList2 soundList;

    [SerializeField]
    Dictionary<string, AudioClip> SoundList;

    void Start()
    {
        
    }
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
    private void Awake()
    {
        var instance = this;
    }

    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip[] audioClipList;

    private AudioClip randomClip;

    public static void PlaySound2(string category, float volume = 1)
    {
        //instance.audioClipList[0];
    }
}