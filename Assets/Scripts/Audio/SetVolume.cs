using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public string mixerExposedVolName;
    public bool music;

    void Start()
    {
        if(music == true)
        {
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }else
        {
            slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        }
        
    }

    public void SetLevel()
    {
        float sliderValue = slider.value;
        mixer.SetFloat(mixerExposedVolName, Mathf.Log10(sliderValue) * 20);
        if(music == true)
        {
           PlayerPrefs.SetFloat("MusicVolume", sliderValue); 
        }else
        {
            PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        }        
    }
}