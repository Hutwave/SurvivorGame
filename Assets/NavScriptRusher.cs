using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDestination : MonoBehaviour
{
    public HazardGen haz;
    public GameObject goal;
    private Transform actualGoal;
    private NavMeshAgent agent;
    private void Awake()
    {
        haz = FindObjectOfType<HazardGen>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        if(actualGoal == null)
        {
            actualGoal = haz.getPlayer().transform;
        }
        agent.destination = actualGoal.position;
    }
}