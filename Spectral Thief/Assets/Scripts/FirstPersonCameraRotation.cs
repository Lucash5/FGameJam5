using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraRotation : MonoBehaviour
{
    public Transform map;

    float distance;
    float distance2;
    float distance3;
    float moved;
    float moved2;
    float moved3;
    public UnityEngine.UI.Button button;
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f; //88

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";

    Transform camera;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        camera = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;


        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        //var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        //camera.localRotation = yQuat;
        transform.localRotation = xQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);


        

        // If the player is close enough, start chasing.
      
            
            
            float raycastdistance = 0.4f;
            //Vector3 direction = player.position - guard.position;

            Ray ray = new Ray(camera.transform.position, -camera.transform.forward);
            Ray ray2 = new Ray(camera.transform.position, camera.transform.right);
            Ray ray3 = new Ray(camera.transform.position, -camera.transform.right);

            RaycastHit hit;
            RaycastHit hit2;
            RaycastHit hit3;

        if (Physics.Raycast(ray, out hit, raycastdistance) && hit.transform.IsChildOf(map))
            {
                Debug.Log(hit.point);
                
            
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z + 0.04f);

            moved += 0.1f;
            }
            else
            {

            float distance = Vector3.Distance(camera.transform.position, hit.point);
            if (moved > 0 && distance > 0.5)
            {
                moved -= 0.1f;
               
               camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z - 0.04f);

            }
            }
        if (Physics.Raycast(ray2, out hit2, raycastdistance) && hit2.transform.IsChildOf(map))
        {

            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x - 0.04f, camera.transform.localPosition.y, camera.transform.localPosition.z);
            moved2 += 0.1f;
        }
        else
        {
            float distance2 = Vector3.Distance(camera.transform.position, hit2.point);
            if (moved2 > 0 && distance2 > 0.5)
            {
                moved2 -= 0.1f;

                camera.transform.localPosition = new Vector3(camera.transform.localPosition.x + 0.04f, camera.transform.localPosition.y, camera.transform.localPosition.z);

            }
        }

        if (Physics.Raycast(ray3, out hit3, raycastdistance) && hit3.transform.IsChildOf(map))
        {

            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x + 0.04f, camera.transform.localPosition.y, camera.transform.localPosition.z);
            moved3 += 0.1f;
        }
        else
        {
            float distance3 = Vector3.Distance(camera.transform.position, hit3.point);
            if (moved3 > 0 && distance3 > 0.5)
            {
                moved3 -= 0.1f;

                camera.transform.localPosition = new Vector3(camera.transform.localPosition.x - 0.04f, camera.transform.localPosition.y, camera.transform.localPosition.z);

            }
        }
        Vector3 startpos = new Vector3(0.226f, 1.748f, -2.598f);
        float distancefromstart = Vector3.Distance( startpos, camera.transform.localPosition);
        if (distancefromstart > 2.9)
        {
            camera.transform.localPosition = startpos;
        }

    }

    
}
