using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSlider : MonoBehaviour
{
    [SerializeField] private AudioClip sliderSound;

    private float previousSoundLevel = 1f;

    private void Start()
    {
        previousSoundLevel = SoundManager.instance.soundSettings.sfxVolume;
    }

    public void PlaySound()
    {
        float currentSoundLevel = SoundManager.instance.soundSettings.sfxVolume;
        if (Mathf.Abs(previousSoundLevel - currentSoundLevel) >= 0.1f)
        {
            previousSoundLevel = currentSoundLevel;
            SoundManager.instance.PlaySfx(sliderSound);
        }
    }
}
