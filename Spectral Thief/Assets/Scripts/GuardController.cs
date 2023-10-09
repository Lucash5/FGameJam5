using UnityEngine;
using UnityEngine.AI;


public class GuardController : MonoBehaviour
{
    public Transform target;         // The player's transform.
    public float chaseDistance = 10f; // Distance at which the guard starts chasing.

    private NavMeshAgent agent;
    private Animator animator;
    private bool isChasing = false;

    bool ghostmode = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the player is close enough, start chasing.
        if (distanceToTarget < chaseDistance && ghostmode == false)
        {
            isChasing = true;
            agent.SetDestination(target.position);
            //animator.SetBool("IsChasing", true);
        }
        else
        {
            isChasing = false;
            //animator.SetBool("IsChasing", false);
        }

        // If the guard is currently chasing, update its destination.
        if (isChasing)
        {
            agent.SetDestination(target.position);
        }

        if (Input.GetKeyDown(KeyCode.E) && ghostmode == false)
        {
            ghostmode = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && ghostmode == true)
        {
            ghostmode = false;
        }
    }
}