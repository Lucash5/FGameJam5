using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GhostModeStamina : MonoBehaviour
{
    public GhostMode script;
    public GuardController guardcontroller;

    [Range(0, 4000)]
    public float stamina;
    public float maxstamina = 2000;

    public RectTransform uiBar;

    float percentunit;
    float staminapercentunit;

    private void Start()
    {
        percentunit = 1f / uiBar.anchorMax.x;
        staminapercentunit = 100f / maxstamina;
    }

    private void Update()
    {
        if (stamina > maxstamina) stamina = maxstamina;
        else if (stamina < 0) stamina = 0;

        float currentstaminapercent = stamina * staminapercentunit;

        uiBar.anchorMax = new Vector2((currentstaminapercent * percentunit) / 100f, uiBar.anchorMax.y);
       
    }

    private void onValidate()
    {
        if (stamina > maxstamina) stamina = maxstamina;
        else if (stamina < 0) stamina = 0;

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(decreasing());
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopCoroutine(decreasing());
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(increasing());
        }
    }

    IEnumerator decreasing()
    {

        if (stamina > 0)
        {
        stamina = stamina - 5;
        }
        else if (stamina == 0)
        {
            script.Nostamina();
            guardcontroller.nostamina();
            StopCoroutine(decreasing());
        }


        
        yield return new WaitForSeconds(1);
    }

    IEnumerator increasing()
    {
        if (stamina < maxstamina)
        {
            stamina = stamina + 5;
        }
        yield return new WaitForSeconds(1);
    }
}
