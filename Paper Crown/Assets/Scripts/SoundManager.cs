using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum SoundType
{
    PLAYER_FOOTSTEPS,
    MUSICBOX,
    PLAYER_JUMP,
    PUSH,
    TOY_BANG,
    PLAYER_HURT,
    BODY_IMPACT,
    MUSICBOX_OPEN,
    TOYBOX_OPEN,
    PLAYER_SNEAK,
    ELEPHANT_SCREAM,
    CLOSET_CREAK,
    TOYBOX_LOCKED,
    KEY_JINGLE,
    TOY_HURT,
    LANTERN_JINGLE,
    SCRIBBLE
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        //instance = this;
    }

    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        if(!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        Save();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        //Debug.Log($"{sound}, {volume},{instance.audioSource.volume}");
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        var index = UnityEngine.Random.Range(0, clips.Length);
        Debug.Log(instance.soundList.Length);
        Debug.Log(index);
        Debug.Log(clips.Length);
        AudioClip randomClip = clips[index];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
            soundList[i].name = names[i];
    }
#endif

    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        audioSource.volume = PlayerPrefs.GetFloat("masterVolume");
    }

    public static void DelayPlaySound(SoundType sound, float volume = 1f, float DelayTime = 1f)
    {
        instance.StartCoroutine(instance.DelaySound(sound, volume, DelayTime));
    }

    public IEnumerator DelaySound(SoundType sound, float volume, float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        PlaySound(sound, volume);
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}