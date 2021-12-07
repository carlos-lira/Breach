using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;

    public SoundSettings soundSettings;
    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one player in scene");
            return;
        }
        instance = this;
        
        GetSoundSettings();
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    void GetSoundSettings()
    {
        soundSettings = new SoundSettings();

        soundSettings.masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        int masterMuted = PlayerPrefs.GetInt("MasterMuted", 0);
        if (masterMuted == 0)
        {
            soundSettings.masterMuted = false;
        }
        else
        {
            soundSettings.masterMuted = true;
        }

        soundSettings.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        int musicMuted = PlayerPrefs.GetInt("MusicMuted", 0);
        if (musicMuted == 0)
        {
            soundSettings.musicMuted = false;
        }
        else
        {
            soundSettings.musicMuted = true;
        }

        soundSettings.sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        int sfxMuted = PlayerPrefs.GetInt("SfxMuted", 0);
        if (sfxMuted == 0)
        {
            soundSettings.sfxMuted = false;
        }
        else
        {
            soundSettings.sfxMuted = true;
        }

    }

    public void SaveMusicSettings()
    {
        Debug.Log("Sound Saved");

        //Saving master
        int masterMuteStatus = soundSettings.masterMuted ? 1 : 0;
        PlayerPrefs.SetFloat("MasterVolume", soundSettings.masterVolume);
        PlayerPrefs.SetInt("MasterMuted", masterMuteStatus);

        //Saving music
        int musicMuteStatus = soundSettings.musicMuted ? 1 : 0;
        PlayerPrefs.SetFloat("MusicVolume", soundSettings.musicVolume);
        PlayerPrefs.SetInt("MusicMuted", musicMuteStatus);

        //Saving SFX
        int sfxMuteStatus = soundSettings.sfxMuted ? 1 : 0;
        PlayerPrefs.SetFloat("SfxVolume", soundSettings.sfxVolume);
        PlayerPrefs.SetInt("SfxMuted", sfxMuteStatus);
    }


}
