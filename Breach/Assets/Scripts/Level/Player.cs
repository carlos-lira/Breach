using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance;
    [SerializeField] private AudioClip hurtSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one player in scene");
            return;
        }

        audioSource = GetComponent<AudioSource>();

        instance = this;
    }
    #endregion

    [SerializeField] private bool alive = true;
    [SerializeField] private float health = 200f;
    [SerializeField] private float money = 300f;

    private void Update()
    {
        audioSource.mute = SoundManager.instance.GetSfxMuteValue();
        audioSource.volume = SoundManager.instance.GetSfxLevel();
    }

    public bool Alive()
    {
        return alive;
    }

    public float CurrentHealth()
    { 
        return health;
    }

    public void DamagePlayer(float damage)
    { 
        health -= damage;

        if (hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        if (health <= 0)
        { 
            alive = false;
            LevelManager.instance.GameOver();
        }

    }

    public void HealPlayer(float heal)
    { 
        health += heal;
    }

    public float MoneyAvailable()
    { 
        return money;
    }

    public void WithdrawMoney(float money)
    { 
        this.money -= money;
    }

    public void AddMoney(float money)
    { 
        this.money += money;
    }

}
