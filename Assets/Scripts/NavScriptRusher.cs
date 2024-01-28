using UnityEngine;
using UnityEngine.AI;

public class MoveDestination : MonoBehaviour
{
    public GameLogic haz;
    private Transform targetLoc;
    private NavMeshAgent agent;
    private float dmgCd;
    private void Awake()
    {
        haz = FindObjectOfType<GameLogic>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        if (targetLoc == null)
        {
            targetLoc = haz.getPlayer().transform;
        }
        agent.destination = targetLoc.position;

        if (targetLoc != null && agent.hasPath && agent.remainingDistance < 1.5f && dmgCd <= 0f)
        {
            agent.isStopped = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(400f, haz.getPlayer().transform.position, 3f);
            this.gameObject.GetComponent<EnemyStats>().damagePlayer();
            dmgCd = 1f;
        }

        if (agent.isStopped && dmgCd < 0.65f)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<SphereCollider>().isTrigger = true;
            agent.isStopped = false;
        }

        if (dmgCd > 0f)
        {
            dmgCd -= Time.deltaTime;
        }
    }
}