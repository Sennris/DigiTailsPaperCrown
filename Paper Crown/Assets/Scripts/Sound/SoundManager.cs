using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

// Finally finished making this new Sound Manager, now you only have to add the category to the
// inspector and not the script itself to make a new category.
// Robson - 3/12/2025

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private int randomIndex;
    private AudioClip randomClip;
    public Slider volumeSlider;
    private static SoundManager instance;

    // Making the "dictionary" visible in inspector
    [SerializeField]
    private SoundListItem[] categories;

    // Declaring the true dictionary
    private Dictionary<string, AudioClip[]> soundDictionary;

    void Start()
    {
        // Allows for static variables or something
        instance = this;

        // Creating the dictionary and copying all of soundList into it
        soundDictionary = new Dictionary<string, AudioClip[]>();
        for (int i = 0; i < categories.Length; i++)
        {
            soundDictionary.Add(categories[i].name, categories[i].audioClipList);
        }

        // Loading volume preferences if the player has any, or setting it to 1 if they don't
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    
    // Play a random audio clip from a given category
    public static void PlaySound(string category, float volume = 1)
    {
        instance.randomIndex = UnityEngine.Random.Range(0, instance.soundDictionary[category].Length);
        instance.randomClip = instance.soundDictionary[category][instance.randomIndex];
        instance.audioSource.PlayOneShot(instance.randomClip);
    }

    // Calls a coroutine to delay a sound
    public static void DelayPlaySound(string category, float volume = 1f, float DelayTime = 1f)
    {
        instance.StartCoroutine(instance.DelaySound(category, volume, DelayTime));
    }

    // Calls the PlaySound function after a specified amount of time
    public IEnumerator DelaySound(string category, float volume, float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        PlaySound(category, volume);
    }

    // Called every time the volume slider is updated
    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        Save();
    }

    // Saves the volume preferences for the player
    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
    }

    // Loads the volume preferences for the player
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        audioSource.volume = PlayerPrefs.GetFloat("masterVolume");
    }
}

// Defining the SoundList class
[Serializable]
public class SoundList
{
    [SerializeField]
    public SoundListItem[] categories;
}

// Defining the items of the SoundList class
[Serializable]
public class SoundListItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip[] audioClipList;
}