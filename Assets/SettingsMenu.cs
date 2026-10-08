using UnityEngine;
using System;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    enum SettingsState{
        Main,
        Audio,
        Video
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
}
