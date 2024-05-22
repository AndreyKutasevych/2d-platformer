using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource _source;
    private AudioSource _musicSource;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance!=null && gameObject!=this)
        {
            Destroy(gameObject);
        }
        ChangeMusicVolume(0);
        ChangeVolume(0);
    }

    public void PlaySound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }

    public void ChangeVolume(float change)
    {
        ChangeSourceVolume(1f,"soundVolume",change,_source);
    }
    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.3f,"musicVolume",change,_musicSource);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float changeValue, AudioSource audioSource)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName);
        currentVolume += changeValue;
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        float finalVolume = currentVolume * baseVolume;
        audioSource.volume = finalVolume;
        PlayerPrefs.SetFloat(volumeName,currentVolume);
    }
}
