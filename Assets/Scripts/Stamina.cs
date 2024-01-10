using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stamina : MonoBehaviour
{
    
   
    [SerializeField] VariableHolder var_Script;
    [SerializeField] Breather breather_Script;
    [SerializeField] PlayFabManager playfab_Script;
    public TextMeshProUGUI stamina_Amount;
    public TextMeshProUGUI Endurance;
    public Slider stamina_Bar;
    [SerializeField] float stamina;
    [SerializeField] float maxStamina;
    public float baseRegenSpeed;
    public float totalRegenSpeed;
    public float drainSpeed;
    private bool draining;
    private bool gainingXP;
    public bool haveStamina;
    private bool atMaxStamina;
    private bool leveling;
    private float endurance;
    public float enduranceXP;
    public float enduranceXPReq;
    private float staminaIncrease;
    private float staminaIncreaseChange;
    private float regenTime;

    public float baseXPAmount;
    public float multXP;
    public float addXP;
    public float totalXPGain;
    public AudioSource levelSound;
    private bool canMultXP;
    private bool canAddXP;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RegenTime", 1, .1f);
        drainSpeed = 2f;
        draining = false;
        atMaxStamina = true;
        staminaIncreaseChange = .1f;
        baseXPAmount = 1;
        canAddXP = true;
        canMultXP = false;
        baseRegenSpeed = .01f;
        
    }

    // Update is called once per frame
    void Update()
    {

        totalRegenSpeed = (baseRegenSpeed * maxStamina) * maxStamina;
        Endurance.text = endurance + "";
        totalXPGain = (baseXPAmount + addXP) * multXP;
  
        staminaIncrease = var_Script.StaminaIncrease;
        enduranceXP = var_Script.EnduranceXP;
        enduranceXPReq = var_Script.EnduranceXPReq;
        endurance = var_Script.Endurance;
        stamina_Bar.value = stamina;
        stamina = var_Script.Stamina;
        stamina_Bar.maxValue = maxStamina;
        var_Script.MaxStamina = maxStamina;

        ChangeMaxStamina(staminaIncrease);
        CheckStamina();
        var_Script.StaminaCap();
        CheckEndurance();
    }

    public void RegenTime()
    {
        
        if (draining == false && PlayFabManager.loggedIn == true)
        {
            regenTime += .1f;
        }
        else
        {
            regenTime = 0;
        }
    }

    public void CheckStamina()
    {
        if(var_Script.Stamina < 0)
        {
            var_Script.Stamina = 0;
        }
        //Draining Check
        {
            draining = true;
        }
        if(breather_Script.breathing == false || stamina <= 0)
        {
            draining = false;
        }
        //Drain
        if (draining == true)
        {
            var_Script.StaminaDrain(drainSpeed);
        }
        //Regen
        else
        {
            var_Script.StaminaRegen(totalRegenSpeed);
        }
        //Have Stamina
        if (stamina > 0)
        {
            haveStamina = true;
        }
        //Don't Have Stamina
        else
        {
            haveStamina = false;
        }
        stamina_Amount.text = $"{Mathf.FloorToInt(Mathf.Abs(stamina)):F0}" + " / " + $"{Mathf.FloorToInt(stamina_Bar.maxValue):F0}";
       
    }
    public void CheckEndurance()
    {
        //At Max
        if(stamina >= maxStamina)
        {
            atMaxStamina = true;
        }
        //Not Max
        else
        {
            atMaxStamina = false;
        }
        //Gain XP
        if (atMaxStamina == false && draining == false && enduranceXP < enduranceXPReq && gainingXP == false && leveling == false && regenTime >= .5f)
        {
        
       
            StartCoroutine(CheckXPGain());
      
        }

        //Gain Levels
        if (enduranceXP >= enduranceXPReq && leveling == false)
        {
            levelSound.Play();
            StartCoroutine(EnduranceUp());
        }
         
    }
   IEnumerator CheckXPGain()
    {
        gainingXP = true;
        var_Script.GainEnduranceXP(totalXPGain);
        yield return new WaitForSeconds(.3f);
        gainingXP = false;
       
       
    }
    IEnumerator EnduranceUp()
    {

     leveling = true;
     var_Script.SetEnduranceXP(enduranceXP - enduranceXPReq);  
     var_Script.AddEnduranceXPReq(5);  
     var_Script.AddEndurance(1);
     var_Script.StaminaIncreaseChange(staminaIncreaseChange);
     leveling = false;
     yield return new WaitForSeconds(1);
    }

    public void ChangeMaxStamina(float staminaincrease)
    {
        maxStamina = (endurance * staminaincrease) + 5 - .2f;
    }
}

