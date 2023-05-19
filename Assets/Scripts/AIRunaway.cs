using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRunaway : MonoBehaviour
{
    // RUNAWAY AI
    // Checks if enemyAwareness isAggro and if it is then do something with the navMesh

    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;

    [SerializeField]
    private float displacementDist = 5f;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyAwareness.isAggro)
        {
            Vector3 normDir = (playersTransform.position - transform.position).normalized;

            //normDir = Quaternion.AngleAxis(45, Vector3.up) * normDir; // Rotate at an angle away from the player
            normDir = Quaternion.AngleAxis(Random.Range(0,179), Vector3.up) * normDir; // move to the right
            MoveToPos(transform.position - (normDir * displacementDist));
        }
        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }

    private void MoveToPos(Vector3 pos)
    {
        enemyNavMeshAgent.SetDestination(pos);
        enemyNavMeshAgent.isStopped = false;
    }
}
