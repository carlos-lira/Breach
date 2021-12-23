using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;

    public void PlaySound()
    { 
        SoundManager.instance.PlaySfx(buttonSound);
    }


}
