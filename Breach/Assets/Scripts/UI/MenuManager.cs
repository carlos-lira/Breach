using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] AudioClip music;

    private void Start()
    {
        SoundManager.instance.PlayMusic(music);
    }
}
