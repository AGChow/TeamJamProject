using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public float newVolumeLevel;

    public Sound[] sounds;

    public static AudioManager instance;

    public AudioSource musicAudioSource;
    private AudioClip _musicAudioClip;
    public AudioClip musicAudioClip {
        get { return _musicAudioClip; }
        set {
            print(value.name);
            musicAudioSource.Stop();
            musicAudioSource.clip = value;
            musicAudioSource.Play();

            _musicAudioClip = value;
        }
    }
    

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Mispelled AudioName");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Mispelled AudioName");
            return;
        }
        s.source.Stop();
    }

    public void VolumeAdjust(string name, float volumeLevel)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Mispelled AudioName");
            return;
        }
        s.source.volume = volumeLevel;
    }

    void Update()
    {
        VolumeAdjustRealTime(newVolumeLevel);
    }
    public void VolumeAdjustRealTime(float newVolumeLevel)
    {
        foreach (Sound Sound in sounds)
        {
            Sound.source.volume = newVolumeLevel;

        }
    }

}