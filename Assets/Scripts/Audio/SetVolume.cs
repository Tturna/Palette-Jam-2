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
    public Text percentageText;

    void Start()
    {
        if(music)
        {
            if(PlayerPrefs.HasKey("MusicVolume"))
            {
                slider.value = PlayerPrefs.GetFloat("MusicVolume");
            }else
            {
                PlayerPrefs.SetFloat("MusicVolume", 0.8f);
                slider.value = PlayerPrefs.GetFloat("MusicVolume");
            }
        }if(!music)
        {
            if(PlayerPrefs.HasKey("SFXVolume"))
            {
                slider.value = PlayerPrefs.GetFloat("SFXVolume");
            }else
            {
                PlayerPrefs.SetFloat("SFXVolume", 0.8f);
                slider.value = PlayerPrefs.GetFloat("SFXVolume");
            }
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

        percentageText.text = Mathf.RoundToInt(sliderValue * 100) + "%";        
    }
}