using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : Turret
{
    [Header("Hourglass Stats")]
    [SerializeField] [Range(0,1)] private float slowFactor = .6f;

    public override void Awake()
    { 
        base.Awake();
        var collider = GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.radius = range;
        }
    }

    public override void Start()
    {
        base.Start();

        //Slow all enemies at range upon placement
    }

    public override void Update()
    {
        base.Update();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < range)
            {
                enemy.GetComponent<Enemy>().ReduceSpeed(slowFactor,attackType);
            }
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReduceSpeed(slowFactor, attackType);
            }
        }
    }
    */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.BackToNormalSpeed();
            }
        }
    }
}
