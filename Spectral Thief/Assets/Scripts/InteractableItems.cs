using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;


public class InteractableItems : MonoBehaviour
{

    public Transform painting;
    public Transform player;
    float distancefromplayer;
    public Canvas canvas;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        distancefromplayer = Vector3.Distance(painting.position, player.position);
        if (distancefromplayer < 3.5f)
        {
            canvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(painting.gameObject);
            }
        }
        else if (distancefromplayer > 3.5f)
        {
            canvas.enabled = false;
        }
    }
}
