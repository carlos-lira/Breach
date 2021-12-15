using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : Turret
{
    [Header("FireTurret Dependencies")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("FireTurret Stats")]
    [SerializeField] private float attackSpeed = 2.0f;
    [SerializeField] private int burnDamage = 1;
    [SerializeField] private float burnDuration = 10;
    [SerializeField] private int numberOfTicks = 5;

    private Transform target;
    private float attackCooldown = 0f;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("UpdateTarget", 0f, 1/attackSpeed);
    }

    public override void Update()
    {
        base.Update();
        if (target == null)
            return;

        if (attackCooldown <= 0f)
        {
            Shoot();
            attackCooldown = 1 / attackSpeed;
        }

        attackCooldown -= Time.deltaTime;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {

            var enemigo = enemy?.GetComponent<Enemy>();

            if (enemigo != null && enemigo.IsAlive() && !enemigo.IsBurned() && !enemigo.IsFireTargeted())
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate<GameObject>(bulletPrefab, firePoint.position, firePoint.rotation);

        if (bullet != null)
        {
            bullet.GetComponent<Projectile>().SetProjectileValues(target, damage, dotDamage: burnDamage, dotDuration: burnDuration, numberOfTicks: numberOfTicks, damageType: attackType);
            target.GetComponent<Enemy>().SetFireTargeted();
        }

    }
}
