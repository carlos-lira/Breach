using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBow : Turret
{
    [Header("Xbow Dependencies")]
    [SerializeField] private Transform partToRotate;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Xbow Stats")]
    [SerializeField] private float attackSpeed = 2.0f;
    [SerializeField] private float turnSpeed = 2.0f;

    private Transform target;
    private float attackCooldown = 0f;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public override void Update()
    {
        base.Update();
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        //Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //lookRotation.eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


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
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
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
            PlayAttackSound();
            bullet.GetComponent<Projectile>().SetProjectileValues(target, damage);
        }

    }
}
