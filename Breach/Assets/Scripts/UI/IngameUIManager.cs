using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{

    public Text moneyText;
    public Text waveText;
    public Text healthText;

    [SerializeField] private WaveSpawner waveSpawner;

    private int currentWave;
    private int totalWaves;

    void Start()
    {
        currentWave = waveSpawner.GetCurrentWave();
        totalWaves = waveSpawner.GetTotalWaves();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoney();
        UpdateWaves();
        UpdateHealth();
    }

    void UpdateMoney()
    {
        moneyText.text = $"{Player.instance.MoneyAvailable().ToString("F0")}$";
    }

    void UpdateWaves()
    {
        currentWave = waveSpawner.GetCurrentWave();
        waveText.text = $"Wave: {currentWave}/{totalWaves}";
    }

    void UpdateHealth()
    {
        healthText.text = $"HP: {Player.instance.CurrentHealth().ToString("F0")}";
    }
}
