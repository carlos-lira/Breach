using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Main Dependencies")]
    public LevelManager levelManager;
    public Animator anim;


    [Header("Main Stats")]
    public float cost = 300f;
    public float range = 15f;
    public int damage = 2;
    [SerializeField] public AttackType attackType = AttackType.PHYSICAL;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public virtual void Awake()
    { 
    }

    public virtual void Start()
    { 
    }


    public virtual void Update()
    {
    }

}
