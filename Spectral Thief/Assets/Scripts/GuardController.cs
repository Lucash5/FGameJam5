using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GuardController : MonoBehaviour
{
    
    bool patrol = false;
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

    Vector3 patrolpathstart;
    public Transform patrolpathend;
    //Vector3 patrolpathend = new Vector3(-2f, 0.923f, 4.749324f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        patrolpathstart = agent.transform.position;

    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the player is close enough, start chasing.
        if (distanceToTarget < chaseDistance && ghostmode == false)
        {
            StartCoroutine(lostvisual());
            lockedon = true;
            float raycastdistance = 16f;
            Vector3 direction = player.position - guard.position;

            Ray ray = new Ray(guard.position, direction);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastdistance))
            {
                Debug.Log("Hit something: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "Player")
                {
                    StopCoroutine(lostvisual());
                    isChasing = true;
                    StopCoroutine(patrolling());
                    patrol = false;
                    agent.SetDestination(target.position);
                    //animator.SetBool("IsChasing", true);
                }


            }
        }
        else
        {
            isChasing = false;
            StartCoroutine(patrolling());
        }

        if (isChasing == true)
        {
            StopCoroutine(patrolling());
            patrol = false;
        }

     
            //animator.SetBool("IsChasing", false);
        

        if (distanceToTarget < 1 && isconfused == false)
        {

            animator.SetFloat("velY", -1);
        }

        // If the guard is currently chasing, update its destination.
        if (isChasing)
        {
            agent.SetDestination(target.position);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && ghostmode == false)
        {
            if (isChasing)
            {
                StartCoroutine(confusion());
            }
            ghostmode = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && ghostmode == true)
        {
            ghostmode = false;
        }

       

            
           
        if (isChasing && distanceToTarget > 2 && isconfused == false)
        {
            StopCoroutine(patrolling());
            animator.SetFloat("velY", 1);
            animator.SetFloat("velX", 0);
        }
        else if (!isChasing && distanceToTarget > 2 && isconfused == false && patrol == false)
        {
            animator.SetFloat("velY", 0);
            animator.SetFloat("velX", 0);
        }

        if (lockedon == true && isChasing == false)
        {
            lockedon = false;
            StartCoroutine(confusion());
        }
        
    }
    public void nostamina()
    {
        ghostmode = false;
    }

    IEnumerator confusion()
    {
        isconfused = true;
        animator.SetFloat("velY", 0);
        animator.SetFloat("velX", 1);
        yield return new WaitForSeconds(3);
        animator.SetFloat("velY", 0);
        animator.SetFloat("velX", 0);
        isconfused = false;
    }
   
    IEnumerator lostvisual()
    {
        yield return new WaitForSeconds(5);
        isChasing = false;
    }


    IEnumerator patrolling()
    {

       
        yield return new WaitForSeconds(3);
        patrol = true;





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
            Debug.Log("amogus");
            animator.SetFloat("velY", 0);
            animator.SetFloat("velX", 0);
            yield return new WaitForSeconds(5);
            agent.SetDestination(patrolpathend.position);
            animator.SetFloat("velY", 1);
            animator.SetFloat("velX", 0);
            
        }
        b = true;
        }

    
}