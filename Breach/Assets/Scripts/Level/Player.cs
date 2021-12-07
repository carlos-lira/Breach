using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one player in scene");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private bool alive = true;
    [SerializeField] private float health = 200f;
    [SerializeField] private float money = 300f;
    
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
