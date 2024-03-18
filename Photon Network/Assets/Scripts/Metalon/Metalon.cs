using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum State
{
    MOVE,
    ATTACK,
    DIE
}

public class Metalon : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform turretPostion;
    [SerializeField] State state;
    [SerializeField] protected int health;
    [SerializeField] protected float maxHealth;
    [SerializeField] HPBar healthBar;
    [SerializeField] Slider HPSlider;
    public int Health
    {
        set { health = value; }
        get { return health; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        turretPostion = GameObject.Find("Turret Tower").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = 100;
        maxHealth = health;
        healthBar = GetComponent<HPBar>();
    }

    void Update()
    {
        switch (state)
        {
            case State.MOVE: Move();
                break;
            case State.ATTACK: Attack();
                break;
            case State.DIE: Die();
                break;
        }
        UpdateHP(health, maxHealth);
    }

    public void UpdateHP(float health, float maxHealth)
    {
        HPSlider.value = health / maxHealth;
    }


    public void Move()
    {
        navMeshAgent.SetDestination(turretPostion.position);
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void Die()
    {
        animator.Play("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Turret Tower"))
        {
            state = State.ATTACK;
        }
    }
}
