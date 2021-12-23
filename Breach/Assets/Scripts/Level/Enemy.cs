using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Animator anim;
    [SerializeField] private Slider slider;


    [Header("Stats")]
    [SerializeField] public AttackType resistanceType = AttackType.NONE;
    [SerializeField] [Range(0,1)] public float resistanceFactor = 0f;
    [SerializeField] public float health = 10;
    [SerializeField] public float initialSpeed = 2.0f;
    [SerializeField] public int damage = 2;
    [SerializeField] public int reward = 10;

    [Header("Debuffs")]
    [SerializeField] private bool isBurned = false;
    [SerializeField] private bool fireTargeted = false;
    private Transform target;
    private int waypointIndex = 0;
    private float currentSpeed = 0f;

    private Renderer renderer;
    private Color initialColor;
    private float initialHealth;

    private void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            initialColor = renderer.material.color;
        }
        anim = GetComponent<Animator>();
        currentSpeed = initialSpeed;
        initialHealth = health;
    }

    private void Start()
    {
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        FaceTarget();

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            //Got to the end, destroy and deal damage
            Player.instance.DamagePlayer(damage);
            Destroy(gameObject);
        }
        else
        {
            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }
    }

    public virtual void Damage(float damage, AttackType damageType) 
    {
        float damageAfterResistance = damage;
        if (damageType == resistanceType)
        { 
            damageAfterResistance = damage * (1 - resistanceFactor);
        }

        this.health -= damageAfterResistance;

        if (slider != null)
            slider.value = health/initialHealth;

        if (health <= 0)
        {
            //Enemy killed, increase cash and destroy
            Player.instance.AddMoney(reward);
            Destroy(gameObject);
        }
    }
    public virtual void Damage(float initialDamage, AttackType damageType, float dotDamage, int numberOfTicks, float debuffDuration) 
    {
        this.Damage(initialDamage, damageType);
        StartCoroutine(DotDamage(dotDamage, damageType, numberOfTicks, debuffDuration));
    }

    public IEnumerator DotDamage(float dotDamage, AttackType damageType, int numberOfTicks, float debuffDuration) 
    {
        if (damageType == AttackType.FIRE)
        {
            SetBurn();
        }

        if (numberOfTicks <= 0)
        {
            //Controlling 0 div and unwanted cases
            numberOfTicks = 1;
        }

        for (int i = 0; i < numberOfTicks; i++)
        {
            this.Damage(dotDamage, damageType);
            yield return new WaitForSeconds(debuffDuration/numberOfTicks);
        }

        //Removing burn after dot expires
        if (damageType == AttackType.FIRE)
        {
            RemoveBurn();
        }
    }

    public void FaceTarget()
    {
        if (this.target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            //Removing Y axis to avoid weird model positioning
            direction = new Vector3(direction.x, 0, direction.z);

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }
    }

    public virtual void Attack() 
    { 
        
    }

    public void ReduceSpeed(float slowFactor, AttackType attackType)
    {
        if (currentSpeed == initialSpeed)
        {
            float slowAfterResistance = slowFactor;
            if (attackType == resistanceType)
            {
                slowAfterResistance = slowFactor * (1 - resistanceFactor);
            }

            currentSpeed *= (1 - slowAfterResistance);
        }
    }

    public void BackToNormalSpeed()
    {
        currentSpeed = initialSpeed;
    }

    public bool IsAlive() 
    { 
        return health > 0;
    }



    public bool IsBurned()
    {
        return isBurned;
    }

    private void SetBurn()
    {
        isBurned = true;
        if (renderer != null)
            renderer.material.color = Color.red;
    }

    private void RemoveBurn()
    {
        isBurned = true;
        fireTargeted = false;

        if (renderer != null)
            renderer.material.color = initialColor;
    }

    public void SetFireTargeted()
    { 
        fireTargeted = true;
    }

    public bool IsFireTargeted()
    {
        return fireTargeted;
    }

    public float GetCurrentSpeed()
    { 
        return currentSpeed;
    }

}
