using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [HideInInspector] public float masterVol;
    [SerializeField] Slider masterVolSlider;
    [HideInInspector] public float hazardVol;
    [SerializeField] Slider hazardVolSlider;
    [HideInInspector] public float musicVol;
    [SerializeField] Slider musicVolSlider;
    [HideInInspector] public int qualityIndex;
    [SerializeField] TMP_Dropdown qualityDropdown;
    [HideInInspector] public bool isFullScreen;
    [SerializeField] Toggle fullScrennToggle;
    public void SetMasterVolume(float masterVol){
        audioMixer.SetFloat("MasterVol", masterVol);
        this.masterVol = masterVol;
        masterVolSlider.value = masterVol;
        SaveFilesManager.instance.currentSaveSlot.masterVol = masterVol;
    }
    public void SetHazardVolume(float hazardVol){
        audioMixer.SetFloat("HazardVol", hazardVol);
        this.hazardVol = hazardVol;
        hazardVolSlider.value = hazardVol;
        SaveFilesManager.instance.currentSaveSlot.hazardVol = hazardVol;
    }
    public void SetMusicVolume(float musicVol){
        audioMixer.SetFloat("MusicVol", musicVol);
        this.musicVol = musicVol;
        musicVolSlider.value = musicVol;
        SaveFilesManager.instance.currentSaveSlot.musicVol = musicVol;
    }
    public void SetQuality (int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
        this.qualityIndex = qualityIndex;
        qualityDropdown.value = qualityIndex;
        SaveFilesManager.instance.currentSaveSlot.qualityIndex = qualityIndex;
    }
    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
        this.isFullScreen = isFullScreen;
        fullScrennToggle.isOn = isFullScreen;
        SaveFilesManager.instance.currentSaveSlot.isFullScreen = isFullScreen;
    }
}
