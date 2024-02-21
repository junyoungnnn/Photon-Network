using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Metalon : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform turretPostion;

    void Start()
    {
        turretPostion = GameObject.Find("Turret Tower").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        navMeshAgent.SetDestination(turretPostion.position);
    }

}
