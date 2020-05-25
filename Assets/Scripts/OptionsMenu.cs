using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volSlider;

    public Toggle soundEffectsToggle;
    public Toggle musicToggle;

    public AudioMixer audioMixer;

    
    private void Start()
    {
        volSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0f);
        audioMixer.SetFloat("volume", volSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", volSlider.value);
        
        soundEffectsToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SoundEffectsToggle", 1));
        if (soundEffectsToggle.isOn)
        {
            audioMixer.SetFloat("soundEffectsVolume", 0f);
            PlayerPrefs.SetInt("SoundEffectsToggle", 1);
        }
        else
        {
            audioMixer.SetFloat("soundEffectsVolume", -80f);
            PlayerPrefs.SetInt("SoundEffectsToggle", 0);
        }
        
        
        musicToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle", 1));
        if (musicToggle.isOn)
        {
            audioMixer.SetFloat("musicVolume", 0f);
            PlayerPrefs.SetInt("MusicToggle", 1);
        }
        else
        {
            audioMixer.SetFloat("musicVolume", -80f);
            PlayerPrefs.SetInt("MusicToggle", 0);
        }
        
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void MuteUnmuteSoundEffects()
    {
        if (!soundEffectsToggle.isOn)
        {
            audioMixer.SetFloat("soundEffectsVolume", -80f);
            PlayerPrefs.SetInt("SoundEffectsToggle", 0);
        }
        else
        {
            audioMixer.SetFloat("soundEffectsVolume", 0f);
            PlayerPrefs.SetInt("SoundEffectsToggle", 1);
        }
    }

    public void MuteUnmuteMusic()
    {
        if (!musicToggle.isOn)
        {
            audioMixer.SetFloat("musicVolume", -80f);
            PlayerPrefs.SetInt("MusicToggle", 0);
        }
        else
        {
            audioMixer.SetFloat("musicVolume", 0f);
            PlayerPrefs.SetInt("MusicToggle", 1);
        }
        
    }

}
