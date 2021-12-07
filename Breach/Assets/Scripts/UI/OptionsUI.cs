using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Toggle masterVolumeToggle;
    public Slider masterVolumeSlider;

    public Toggle musicVolumeToggle;
    public Slider musicVolumeSlider;

    public Toggle sfxVolumeToggle;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        masterVolumeToggle.isOn = !SoundManager.instance.soundSettings.masterMuted;
        masterVolumeSlider.value = SoundManager.instance.soundSettings.masterVolume;

        musicVolumeToggle.isOn = !SoundManager.instance.soundSettings.musicMuted;
        musicVolumeSlider.value = SoundManager.instance.soundSettings.musicVolume;

        sfxVolumeToggle.isOn = !SoundManager.instance.soundSettings.sfxMuted;
        sfxVolumeSlider.value = SoundManager.instance.soundSettings.sfxVolume;
    }

    private void OnDisable()
    {
        SoundManager.instance.SaveMusicSettings();
    }

    public void MuteMaster()
    {
        SoundManager.instance.soundSettings.masterMuted = !masterVolumeToggle.isOn;
    }

    public void MuteMusic()
    {
        SoundManager.instance.soundSettings.musicMuted = !musicVolumeToggle.isOn;
    }

    public void MuteSfx()
    {
        SoundManager.instance.soundSettings.sfxMuted = !sfxVolumeToggle.isOn;
    }

    public void UpdateMasterVolume()
    { 
        SoundManager.instance.soundSettings.masterVolume = masterVolumeSlider.value;
    }

    public void UpdateMusicVolume()
    {
        SoundManager.instance.soundSettings.musicVolume = musicVolumeSlider.value;
    }

    public void UpdateSfxVolume()
    {
        SoundManager.instance.soundSettings.sfxVolume = sfxVolumeSlider.value;
    }
}
