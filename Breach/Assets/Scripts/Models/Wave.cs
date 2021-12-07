using System;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField] float secondsBetweenEnemies = 1f;
    [SerializeField] Transform[] enemies;

    public Transform[] GetEnemies()
    {
        return enemies;
    }

    public int GetNumberOfEnemies()
    {
        return enemies?.Length ?? 0;
    }

    public Transform GetEnemy(int position)
    { 
        return enemies[position];
    }

    public float GetSecondsBetweenEnemies()
    { 
        return secondsBetweenEnemies;
    }

}

