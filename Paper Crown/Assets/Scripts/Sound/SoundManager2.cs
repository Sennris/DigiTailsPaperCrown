using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SoundManager2 : MonoBehaviour
{
    public AudioSource audioSource;
    private int randomIndex;
    private AudioClip randomClip;

    [SerializeField]
    private SoundList2 soundList;

    private Dictionary<string, AudioClip[]> soundDictionary;

    void Start()
    {
        soundDictionary = new Dictionary<string, AudioClip[]>();
        Debug.Log(soundList);
        Debug.Log(soundList.categories);
        Debug.Log(soundList.categories[0].name);
        Debug.Log(soundList.categories[0].audioClipList);
        Debug.Log(soundDictionary);
        for (int i = 0; i < 2; i++)
        {
            soundDictionary.Add(soundList.categories[i].name, soundList.categories[i].audioClipList);
        }

        PlaySound2("playerjumping");
    }
    
    public void PlaySound2(string category, float volume = 1)
    {
        randomIndex = UnityEngine.Random.Range(0, soundDictionary.Count);
        randomClip = soundDictionary[category][randomIndex];
        audioSource.PlayOneShot(randomClip);
    }
}

[Serializable]
public class SoundList2
{
    [SerializeField]
    public SoundListItem[] categories;
}

[Serializable]
public class SoundListItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip[] audioClipList;
}