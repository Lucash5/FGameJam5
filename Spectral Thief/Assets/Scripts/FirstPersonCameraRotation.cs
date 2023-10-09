using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraRotation : MonoBehaviour
{

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
    }
}
