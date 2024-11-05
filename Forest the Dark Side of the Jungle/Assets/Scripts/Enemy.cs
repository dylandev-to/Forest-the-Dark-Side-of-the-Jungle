using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum AIStates
    {
        Idle,
        Wandering,
        Chasing,
        Attacking
    }

    [SerializeField]
    private NavMeshAgent agent = null;

    [SerializeField]
    private LayerMask floorMask = 0;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float chaseDistance = 10.0f;

    [SerializeField]
    private float attackDistance = 2.0f;

    [SerializeField]
    private float attackCooldown = 1.0f;

    private AIStates curState = AIStates.Idle;
    private float waitTimer = 0.0f;
    private float attackTimer = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            curState = AIStates.Attacking;
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            curState = AIStates.Chasing;
        }
        else if (curState != AIStates.Wandering)
        {
            curState = AIStates.Idle;
        }

        switch (curState)
        {
            case AIStates.Idle:
                DoIdle();
                break;
            case AIStates.Wandering:
                DoWander();
                break;
            case AIStates.Chasing:
                DoChase();
                break;
            case AIStates.Attacking:
                DoAttack();
                break;
            default:
                Debug.LogError("Error.");
                break;
        }

        animator.SetBool("isMoving", agent.velocity.magnitude > 0.1f);
    }

    private void DoIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        agent.SetDestination(RandomNavSphere(transform.position, 10.0f, floorMask));
        curState = AIStates.Wandering;
    }

    private void DoWander()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer = Random.Range(1.0f, 4.0f);
            curState = AIStates.Idle;
        }
    }

    private void DoChase()
    {
        agent.SetDestination(player.position);
    }

    private void DoAttack()
    {
        if (attackTimer <= 0f)
        {
            agent.SetDestination(transform.position);
            animator.SetTrigger("Attack");
            attackTimer = attackCooldown;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackDistance)
        {
            curState = AIStates.Chasing;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance + origin;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, distance, layerMask);
        return navHit.position;
    }
}
