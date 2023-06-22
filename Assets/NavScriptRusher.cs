using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDestination : MonoBehaviour
{
    public GameLogic haz;
    private Transform targetLoc;
    private NavMeshAgent agent;
    private void Awake()
    {
        haz = FindObjectOfType<GameLogic>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        if(targetLoc == null)
        {
            targetLoc = haz.getPlayer().transform;
        }
        agent.destination = targetLoc.position;
    }
}