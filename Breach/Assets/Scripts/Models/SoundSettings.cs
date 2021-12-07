using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundSettings
{
    public bool masterMuted = false;
    [Range(0f, 1f)]
    public float masterVolume = 1f;

    public bool musicMuted = false;
    [Range(0f, 1f)]
    public float musicVolume = 1f;

    public bool sfxMuted = false;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

}