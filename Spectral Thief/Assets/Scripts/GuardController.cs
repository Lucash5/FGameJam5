using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class GuardController : MonoBehaviour
{

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

    public Transform patrolpathstart;
    
    public Transform patrolpathend;
    //Vector3 patrolpathend = new Vector3(-2f, 0.923f, 4.749324f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        

        agent.SetDestination(patrolpathend.position);
        StartCoroutine(jogging());

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
                    
                    lost = false;
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
            
        }
   

        if (isChasing == true)
        {
            lost2 = true;
            lost = false;
            StartCoroutine(jogging());
            StopCoroutine(patrolling());
            patrol = false;
        }
        else if (isChasing == false)
        {
            lost = true;
            StartCoroutine(patrolling());
        }

        if (lost == true && lost2 == true)
        {
            lost2 = false;
            StartCoroutine(confusion());
        }

        //animator.SetBool("IsChasing", false);


        if (distanceToTarget < 1)
        {

            StartCoroutine(catching());
            StartCoroutine(resetgame());

        }

        // If the guard is currently chasing, update its destination.
        if (isChasing)
        {
            agent.SetDestination(target.position);
        }

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
        isconfused = true;
        animator.Play("Confusion");
        yield return new WaitForSeconds(3);
        isconfused = false;
    }

    IEnumerator lostvisual()
    {
        lost = true;
        Debug.Log("start");
        yield return new WaitForSeconds(5);
        if (lost == true)
        {
        Debug.Log("Commence");

        isChasing = false;
        StartCoroutine(confusion());
        StartCoroutine(patrolling());
        }
    }

    bool commencing = false;
    bool a = false;
    bool b = false;

    IEnumerator patrolling()
    {
        yield return new WaitForSeconds(3);
        if (!commencing)
        {
            
            commencing = true;
            
         

            float distance = Vector3.Distance(transform.position, patrolpathend.position);
            yield return new WaitForSeconds(2);
            if (!a)
            {
                a = true;
                //Debug.Log("1");
                StartCoroutine(idling());
                yield return new WaitForSeconds(5);
                agent.SetDestination(patrolpathstart.position);
                StartCoroutine(jogging());
            }
            
            float distance2 = Vector3.Distance(agent.destination, transform.position);
            yield return new WaitForSeconds(2);
            if (!b)
            {
                b = true;
                //Debug.Log("2");
                StartCoroutine(idling());
                yield return new WaitForSeconds(5);
                agent.SetDestination(patrolpathend.position);
                StartCoroutine(jogging());
            }


            if (a && b)
            {
                a = false;
                b = false;
            commencing = false;
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

    IEnumerator jogging()
    {

       
        animator.Play("Jog");
        yield return new WaitForSeconds(0);
        
    }

    IEnumerator idling()
    {

       
         
            animator.Play("Idle");
            yield return new WaitForSeconds(0);
        
    }
    IEnumerator catching()
    {



        animator.Play("Catch");
        yield return new WaitForSeconds(0);

    }


    IEnumerator resetgame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SampleScene");
    }

}