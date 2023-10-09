using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GhostModeStamina : MonoBehaviour
{


    [Range(0, 4000)]
    public int stamina;
    public int maxstamina = 2000;

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
       
    }
}
