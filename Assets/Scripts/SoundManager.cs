using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip music;

    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    void Awake()
    {
        // Implementing Singleton pattern for the SoundManager
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Load all sound effects into the dictionary from your Resources/Sounds folder
        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sounds");
        foreach (var clip in sounds)
        {
            soundEffects[clip.name] = clip;
        }
        if (!musicSource.isPlaying)
        {
            PlayMusic(music);
            Debug.Log("Music Starts!");
        }
    }

    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        musicSource.clip = musicClip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string name, float volume = 1.0f)
    {
        if (soundEffects.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("Sound effect not found in dictionary: " + name);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
