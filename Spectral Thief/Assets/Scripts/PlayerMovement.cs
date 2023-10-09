using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 playerInput;
    Rigidbody rb;
    int jumpForce;
    int realSPeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y > -0.05f && rb.velocity.y < 0.05f)
        {
            rb.AddForce(Vector3.up * jumpForce);


        }
    }
    private void walkspeed()
    {

        playerInput = transform.forward * Input.GetAxis("Vertical") * realSPeed;
        playerInput += transform.right * Input.GetAxis("Horizontal") * realSPeed;
        playerInput.y = rb.velocity.y;
    }
}
