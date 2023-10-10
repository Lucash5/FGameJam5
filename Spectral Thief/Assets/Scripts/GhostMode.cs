using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GhostMode : MonoBehaviour
{
    bool mode = false;

    
    

    public AudioClip AC;
    public AudioSource AS;
    public SkinnedMeshRenderer smr1;
    public SkinnedMeshRenderer smr2;
    public SkinnedMeshRenderer smr3;

    public Material material1;
    public Material material2;
    public Material material3;
    public Camera camera;

    Material ogmaterial1;
    Material ogmaterial2;
    Material ogmaterial3;
    void Start()
    {
        ogmaterial1 = smr1.material;
        ogmaterial2 = smr2.material;
        //ogmaterial3 = smr3.material;
    }

  
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.LeftShift) && mode == false)
        {
            //float gamma = 1 - (0.1f * Time.deltaTime);
            //camera.transform.gameObject.GetComponent<PostProcessVolume>().get = new Vector4(gamma,0,0,0);
            //ColorGrading colorGrading;
            /*if (camera.transform.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out colorGrading))
            {
                // Modify the gamma value
                
                //colorGrading.gamma.value = new Vector4(gamma, 0, 0, 0); // Change the values as needed
            }*/
            camera.transform.gameObject.layer = 6;
            smr1.material = material1;
            smr2.material = material2;
            //smr3.material = material3;
            mode = true;
           
            AS.Play();
            
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && mode == true)
        {
            camera.transform.gameObject.layer = 0;
            smr1.material = ogmaterial1;
            smr2.material = ogmaterial2;
            //smr3.material = ogmaterial3;
            mode = false;
            AS.Stop();
        }


    }
    public void Nostamina()
    {
        camera.transform.gameObject.layer = 0;
        smr1.material = ogmaterial1;
        smr2.material = ogmaterial2;
        mode = false;
        AS.Stop();
    }
    
}
