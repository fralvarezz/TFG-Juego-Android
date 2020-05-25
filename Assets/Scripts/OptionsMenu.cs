using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volSlider;

    public AudioMixer audioMixer;
    
    private void Start()
    {
        volSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0f);
        audioMixer.SetFloat("volume", volSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", volSlider.value);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        //PlayerPrefs.Save();
    }
}
