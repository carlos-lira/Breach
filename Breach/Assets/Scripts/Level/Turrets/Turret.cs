using System;
using UnityEngine;
using UnityEngine.Audio;

public class Turret : MonoBehaviour
{
    [Header("Main Dependencies")]
    public LevelManager levelManager;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioMixerGroup audioMixerGroup;


    [Header("Main Stats")]
    public float cost = 300f;
    public float range = 15f;
    public int damage = 2;
    [SerializeField] public AttackType attackType = AttackType.PHYSICAL;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public virtual void Awake()
    { 
        audioMixerGroup = GameManager.instance.audioMixerGroup;
        audioSource = GetComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = audioMixerGroup;

        audioSource.mute = SoundManager.instance.soundSettings.sfxMuted;
        audioSource.volume = SoundManager.instance.soundSettings.sfxVolume;
    }

    public virtual void Start()
    { 
    }


    public virtual void Update()
    {
        audioSource.mute = SoundManager.instance.GetSfxMuteValue();
        audioSource.volume = SoundManager.instance.GetSfxLevel();

        if (Time.timeScale == 0)
        {
            audioSource.Pause();
        }
        else
        { 
            audioSource.UnPause();
        }
    }

    public virtual void PlayAttackSound()
    { 
        audioSource.PlayOneShot(attackSound);
    }

}
