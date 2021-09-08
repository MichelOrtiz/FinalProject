using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMasterVolume(float masterVol){
        audioMixer.SetFloat("MasterVol", masterVol);
    }
    public void SetHazardVolume(float hazardVol){
        audioMixer.SetFloat("HazardVol", hazardVol);
    }
    public void SetMusicVolume(float musicVol){
        audioMixer.SetFloat("MusicVol", musicVol);
    }
    public void SetQuality (int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }
}
