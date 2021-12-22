using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{

    [Header("Laser Dependencies")]
    [SerializeField] public Transform firePoint;
    [SerializeField] private ParticleSystem laserImpactEffect;
    [SerializeField] private Light laserImpactLight;

    [Header("Laser Stats")]
    [SerializeField] private float damageInterval = 0.5f;

    private List<Transform> enemiesInRange = new List<Transform>();
    private Transform target = null;
    private Enemy enemy = null;
    private LineRenderer lineRenderer;
    private int currentDamage = 0;
    
    public override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();

        var collider = GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.radius = range;
        }

        audioSource.clip = attackSound;
        audioSource.loop = true;
    }

    public override void Start()
    {
        base.Start();
        currentDamage = damage;
    }
    
    public override void Update()
    {
        base.Update();

        if (!enemy?.IsAlive() ?? false)
        {
            RemoveTarget();
        }
        
        if (target == null && enemiesInRange.Count > 0)
        { 
            SetTarget();
        }
        
        if (target != null)
        {
            if (! (enemy?.IsAlive() ?? true ) )
            {
                RemoveTarget();
            }
            else 
            {
                UpdateLaser();
            }
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            RemoveTarget();
        }
        else
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        laserImpactEffect.transform.position = target.position;

        Vector3 dir = firePoint.position - target.position;
        laserImpactEffect.transform.position = target.position + dir.normalized;
        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void SetTarget()
    {
        if (enemiesInRange.Count > 0)
        {
            Debug.Log(enemiesInRange.Count);
            Debug.Log("Attempting target selection");

            target = SelectTarget();

            if (target != null)
            {
                Debug.Log("Target selected");
                enemy = target.GetComponent<Enemy>();
                lineRenderer.enabled = true;
                laserImpactEffect.Play();
                laserImpactLight.enabled = true;
                audioSource.Play();
                StartCoroutine(DealDamageOverTime());
            }
        }
    }

    private Transform SelectTarget()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i] != null)
            { 
                return enemiesInRange[i].transform;
            }
        }

        return null;
    }

    private void RemoveTarget()
    {
        Debug.Log("Removing target");
        enemiesInRange.Remove(target);
        target = null;
        enemy = null;
        lineRenderer.enabled = false;
        laserImpactEffect.Stop();
        laserImpactLight.enabled = false;
        
        currentDamage = damage;
        audioSource.Stop();
        StopCoroutine(DealDamageOverTime());
    }

    private IEnumerator DealDamageOverTime()
    {
        while (target != null && (enemy?.IsAlive() ?? false))
        {
            Debug.Log("Dealing damage: " + currentDamage);
            enemy.Damage(currentDamage, attackType);
            currentDamage *= 2;

            if (!enemy.IsAlive())
            { 
                RemoveTarget();
                currentDamage = damage;
                break;
            }
            
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
