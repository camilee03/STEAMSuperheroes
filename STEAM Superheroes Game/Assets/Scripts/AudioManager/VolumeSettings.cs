using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // [SerializeField] private GameObject[] ObjectsToHide;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    [SerializeField] private float defaultVolume = 0.5f;

    public void Awake()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void InitializeVolume()
    {
        MasterVolumeSlider.value = defaultVolume;
        MusicVolumeSlider.value = defaultVolume;
        SFXVolumeSlider.value = defaultVolume;

        if (PlayerPrefs.HasKey("masterVolume")) MasterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        else PlayerPrefs.SetFloat("masterVolume", MasterVolumeSlider.value);

        if (PlayerPrefs.HasKey("musicVolume")) MusicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        else PlayerPrefs.SetFloat("musicVolume", MusicVolumeSlider.value);

        if (PlayerPrefs.HasKey("sfxVolume")) SFXVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        else PlayerPrefs.SetFloat("sfxVolume", SFXVolumeSlider.value);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        float masterVolume = MasterVolumeSlider.value;
        audioMixer.SetFloat("MasterVolume", MathF.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("masterVolume", MasterVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        float musicVolume = MusicVolumeSlider.value;
        audioMixer.SetFloat("MusicVolume", MathF.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", MusicVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = SFXVolumeSlider.value;
        audioMixer.SetFloat("SFXVolume", MathF.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", SFXVolumeSlider.value);
    }
}
