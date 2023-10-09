using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 350;
    public float jumpForce = 350;

    Rigidbody rb;
    Vector3 playerInput;
    Animator animator;
   
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

   
    void Update()
    {



        float realSPeed = speed;

        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        playerInput = transform.forward * inputY * realSPeed;
        playerInput += transform.right * inputX * realSPeed;
        playerInput.y = rb.velocity.y;


        /*if (rb.velocity.y > -0.05f && rb.velocity.y < 0.05f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce);
            }
        }*/

        animator.SetFloat("velX", inputX);
        animator.SetFloat("velY", inputY);
        

    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput;
    }

}
