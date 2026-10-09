using UnityEngine;
using System;
using System.Collections.Generic;
using ServiceLocatorPattern;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    enum SettingsState{
        Main,
        Audio,
        Video
    }

    [SerializeField] private Dictionary<string, AudioMixer> mixers = new Dictionary<string, AudioMixer>(){
        ["MasterVolume"] = null,
        ["MusicVolumeMixer"] = null,
        ["SFXVolume"] = null
    };

    [SerializeField] private Dictionary<string, Slider> sliders = new Dictionary<string, Slider>(){
        ["MasterVolume"] = null,
        ["MusicVolume"] = null,
        ["SFXVolume"] = null
    };

    [SerializeField] private Slider brightnessSlider;
    private BrightnessOverlay brightness
    {
        get
        {
            return ServiceLocator.GetService(typeof(BrightnessOverlay)).GetComponent<BrightnessOverlay>();;
        }
    }

    private SettingsState state;
    private SettingsState State 
    {
        get 
        {
            return state;
        }

        set
        {
            state = value;
            SetCurrentPanel();
        }
    }

    [SerializeField] private Dictionary<SettingsState, GameObject> panels = new Dictionary<SettingsState, GameObject>()
    {
        {SettingsState.Main, null},
        {SettingsState.Audio, null},
        {SettingsState.Video, null}
    };

    void Start()
    {
        if (brightnessSlider == null || brightness == null){return;}
        brightnessSlider.onValueChanged.AddListener(brightness.SetAlpha);
        brightnessSlider.value = brightness.GetAlpha();

        sliders["MasterVolume"].value = GetVolume("MasterVolume");
        sliders["MusicVolume"].value = GetVolume("MusicVolume");
        sliders["SFXVolume"].value = GetVolume("SFXVolume");

        sliders["MasterVolume"].onValueChanged.AddListener(SetMasterVolume);
        sliders["MusicVolume"].onValueChanged.AddListener(SetMusicVolume);
        sliders["SFXVolume"].onValueChanged.AddListener(SetSFXVolume);
    }


    void OnEnable()
    {
        State = SettingsState.Main;
    }

    public void MainBackButtonPressed()
    {
        gameObject.SetActive(false);
    }

    public void AudioBackButtonPressed()
    {
        State = SettingsState.Main;
    }

    public void VideoBackButtonPressed()
    {
        State = SettingsState.Main;
    }

    public void VideoButtonPressed()
    {
        State = SettingsState.Video;
    }

    public void AudioButtonPressed()
    {
        State = SettingsState.Audio;
    }

    private void SetCurrentPanel()
    {
        foreach(SettingsState key in panels.Keys)
        {
            if(panels[key] == null)
            {
                continue;
            }

            if (key == state)
            {
                panels[key].SetActive(true);
            }
            else
            {
                panels[key].SetActive(false);
            }
        }
    }

    public float GetVolume(string mixerName)
    {
        if(!mixers.ContainsKey(mixerName)){return 0f;}
        if(mixers[mixerName] == null){return 0f;}

        if (mixers[mixerName].GetFloat(mixerName, out float dbVolume))
        {
            return Mathf.Pow(10f, dbVolume / 20f);
        }

        return 0f;
        
    }

    public void SetMasterVolume(float vol)
    {
        float linearVolume = Mathf.Clamp(vol, 0.0001f, 1f);
        float dbVolume = Mathf.Log10(linearVolume) * 20;
        mixers["MasterVolume"].SetFloat("MasterVolume", dbVolume);
    }

    public void SetMusicVolume(float vol)
    {
        float linearVolume = Mathf.Clamp(vol, 0.0001f, 1f);
        float dbVolume = Mathf.Log10(linearVolume) * 20;
        mixers["MusicVolume"].SetFloat("MusicVolume", dbVolume);
    }

    public void SetSFXVolume(float vol)
    {
        float linearVolume = Mathf.Clamp(vol, 0.0001f, 1f);
        float dbVolume = Mathf.Log10(linearVolume) * 20;
        mixers["SFXVolume"].SetFloat("SFXVolume", dbVolume);
    }

    
}
