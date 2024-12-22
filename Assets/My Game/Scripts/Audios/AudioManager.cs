using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{ 
    public static AudioManager Instance;

    public AudioMixer audioMixer;

    public Sound[] musicSounds, sfxSounds,sfxLoopSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource,sfxLoop;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SFXVolume(Pref.VolumeSFX);
        MusicVolume(Pref.VolumeMusic);


        PlayMusic(NameSound.Music01.ToString());

        
    }
    public void PlayMusic(string name)
    {
        if (musicSounds == null || musicSource == null || musicSounds.Length <= 0) return;
        Sound s = Array.Find(musicSounds, a => a.name == name);

        if (s == null)
            return;
        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();
        }
       
    }

    public void PlaySFX(string name)
    {
        if (sfxSounds == null || sfxSource == null || sfxSounds.Length <= 0) return;
        Sound s = Array.Find(sfxSounds, a => a.name == name);
        if (s == null)
            return ;
        else {
            sfxSource.PlayOneShot(s.clip);

        }
    }
    public void PlaySFX(AudioSource audioSource,AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null) return;
        audioSource.PlayOneShot(audioClip);
    }
    public void PlaySFXLoop(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null) return;
        // Chỉ phát nếu âm thanh chưa được phát
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.volume = Pref.VolumeSFX;
            audioSource.Play();
        }
    }

    public void StopSFXLoop()
    {
        if (sfxLoop && sfxLoop.isPlaying)
        {
            sfxLoop.Stop();
        }
    }
    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }
    public void ToggleMusic()
    {
        if (musicSource != null)
            musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        if (sfxSource != null)
        {
            sfxSource.mute = !sfxSource.mute;
        }
    }
    public void MusicVolume(float volume)
    {
        if(audioMixer != null)
        {
            audioMixer.SetFloat("MusicVolume",MathF.Log10(volume)*20);
            Pref.VolumeMusic = volume;
        }
           
        
    }
    public void SFXVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFXVolume", MathF.Log10(volume) * 20);
            Pref.VolumeSFX = volume;
        }
    }

}

public enum NameSound
{//player
    Jump,
    Land,
    Hurt,
    Death,
    Punch,
    DoublePunch,
    Throw,
    Bow,
    SlashSword,

    // âm thanh do destroy nhanh, thiếu hppotion
    CollectCoin,
    CollectKey,
    WoodBreak,
    //UI
    UIClick,
    Win,
    Gameover,
    Message,
    RequireDialog,
    //Theme
    Music01,

}