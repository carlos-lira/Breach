using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public int damage = 0;
    public int dotDamage = 0;
    public float dotDuration = 0f;
    public int numberOfTicks = 1;
    public AttackType damageType = AttackType.PHYSICAL;

    public void SetProjectileValues(Transform target, int damage, int dotDamage = 0, float dotDuration = 0f, int numberOfTicks = 0, AttackType damageType = AttackType.PHYSICAL)
    {
        this.target = target;
        this.damage = damage;
        this.dotDamage = dotDamage;
        this.dotDuration = dotDuration;
        this.numberOfTicks = numberOfTicks;
        this.damageType = damageType;
    }

    public void SetTarget(Transform target) 
    { 
        this.target = target;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    
    void Update()
    {
        if (target == null)
        { 
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float currentDistance = speed * Time.deltaTime;

        if (dir.magnitude <= currentDistance)
        {
            HitTarget();
            return;
        }

        transform.rotation = Quaternion.LookRotation(dir);
        transform.Translate(dir.normalized * currentDistance, Space.World);
    }

    private void HitTarget()
    {
        Destroy(gameObject);

        if (dotDamage > 0)
        {
            //This is a dot
            target.GetComponent<Enemy>().Damage(damage, damageType, dotDamage, numberOfTicks, dotDuration);
        }
        else 
        {
            //This is regular damage
            target.GetComponent<Enemy>().Damage(damage, damageType);
        }
    }

}
