using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GuardController : MonoBehaviour
{
    bool hasfound = false;

    AudioSource aaaaa;
    bool lost = true;
    bool lost2 = false;

    bool patrol = true;
    bool isconfused = false;
    bool lockedon = false;

    public Transform player;
    public Transform guard;


    public Transform target;         // The player's transform.
    public float chaseDistance = 10f; // Distance at which the guard starts chasing.

    private Rigidbody rb;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isChasing = false;

    bool ghostmode = false;
    bool isIdling = false;
    public Transform[] patrolPoints;

    int currentPatrolIndex = 0;

    //Vector3 patrolpathend = new Vector3(-2f, 0.923f, 4.749324f);

    EnemyState state;

    public bool Isconfused
    {
        get => isconfused;
        set
        {
            isconfused = value;
            animator.SetBool("isConfused", value);
        }
    }

    void Start()
    {
        aaaaa = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (patrolPoints.Length > 1)
        {
            agent.SetDestination(patrolPoints[1].position);
            state = EnemyState.Patrol;
        }
        else
        {
            state = EnemyState.Idle;
        }
    }

    void Update()
    {
        //animator.SetBool("IsMoving", agent.velocity.magnitude > 1f);
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the player is close enough, start chasing.
        if (distanceToTarget < chaseDistance && ghostmode == false)
        {


            lockedon = true;
            float raycastdistance = 16f;
            Vector3 direction = player.position - guard.position;

            Ray ray = new Ray(guard.position, direction);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastdistance))
            {
                //Debug.Log("Hit something: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "Player")
                {

                    state = EnemyState.Chasing;
                }
            }
        }
        else if (state == EnemyState.Chasing)
        {
            state = EnemyState.Confused;
        }


        switch (state)
        {
            case EnemyState.Idle:
                if (!isIdling)
                    StartCoroutine(Idle());
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Confused:
                if (!Isconfused)
                    StartCoroutine(confusion());
                break;
            case EnemyState.Chasing:
                agent.SetDestination(player.position);
                if (Isconfused)
                {
                    Isconfused = false;
                    StopCoroutine(confusion());
                }

                animator.SetFloat("velocity", agent.velocity.magnitude);

                if (isIdling)
                {
                    isIdling = false;
                    StopCoroutine(Idle());
                }

                agent.isStopped = false;



                break;
        }
                if (Vector3.Distance(player.position, this.transform.position) < agent.stoppingDistance)
                {
                    StartCoroutine(Catch());
                }

        if (state == EnemyState.Chasing && hasfound == false)
        {
            hasfound = true;
            aaaaa.Play();
        }

        //animator.SetBool("IsChasing", false);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ghostmode = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ghostmode = false;
        }

    }
    public void nostamina()
    {
        ghostmode = false;
    }

    IEnumerator confusion()
    {
        Isconfused = true;
        animator.Play("Confusion", 0);
        yield return new WaitForSeconds(3);
        state = EnemyState.Patrol;
        Isconfused = false;
    }

    public void Patrol()
    {
        if (patrolPoints.Length > 1)
        {
            if (currentPatrolIndex == patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }
            if (Vector3.Distance(this.transform.position, patrolPoints[currentPatrolIndex].position) < agent.stoppingDistance)
            {

                state = EnemyState.Idle;
                // Stopped at the patrol point
                agent.isStopped = true;
                currentPatrolIndex++;

                animator.SetFloat("velocity", 0);
            }
            else
            {
                
                agent.isStopped = false;
                animator.SetFloat("velocity", agent.velocity.magnitude);
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }
    }


    /*IEnumerator patrolling()
    {

        
        yield return new WaitForSeconds(3);
        patrol = true;



        bool commencing = false;
        if (commencing == false)
        {
            commencing = true;
        float distance = Vector3.Distance(transform.position, patrolpathend.position);
        bool a = false;
        if (distance < 0.5 && a == false)
        {
            animator.SetFloat("velY", 0);
            animator.SetFloat("velX", 0);
            yield return new WaitForSeconds(5);
            agent.SetDestination(patrolpathstart);
            animator.SetFloat("velY", 1);
            animator.SetFloat("velX", 0);
        }

        a = true;
        float distance2 = Vector3.Distance(transform.position, patrolpathstart);
        bool b = false;
        if (distance2 < 0.5 && b == false)
        {
            animator.SetFloat("velY", 0);
            animator.SetFloat("velX", 0);
            yield return new WaitForSeconds(5);
            agent.SetDestination(patrolpathend.position);
            animator.SetFloat("velY", 1);
            animator.SetFloat("velX", 0);
            
        }
        b = true;
        }
        commencing = false;
    }*/

    IEnumerator Idle()
    {
        isIdling = true;
        yield return new WaitForSeconds(3);
        state = EnemyState.Patrol;
        isIdling = false;
    }

    IEnumerator Catch()
    {
        animator.Play("Catch", 0);
        state = EnemyState.Catch;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(resetgame());
    }


    IEnumerator resetgame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Map");
        Debug.Log("AMOGUS AGOUAGMOAGUS");
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        if (patrolPoints.Length > 0)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (i + 1 < patrolPoints.Length)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
            }
        }
    }
}

public enum EnemyState
{
    Idle,
    Patrol,
    Confused,
    Chasing,
    Catch
}