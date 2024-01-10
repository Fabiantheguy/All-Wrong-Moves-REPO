using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{

    public AudioSource selectSound;
    public AudioSource declineSound;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
      
    }
    public void UpgradeStyleNode(string curUpgrade, float cost, float curCurrency)
    {

        if (curCurrency >= cost)
        {
            selectSound.Play();

            curUpgrade += 1f;

        }
        else
        {
            declineSound.Play();
        }


    }
    public void UpgradeStatNode(string curUpgrade, float cost, float curCurrency)
    {

        if (/*power points */curCurrency >= cost)
        {
            selectSound.Play();


        }
        else
        {
            declineSound.Play();
        }


    }


    /*Text Math
    staminaRegenLevelText.text = "Stamina Regen Lv." + seenStaminaRegenLevel;
    staminaRegenUpgradeAmountText.text = "+" + seenStaminaRegenUpgradeAmount + "%";
    staminaRegenUpgradeTotalText.text = 1f + Mathf.Round(seenStaminaRegenUpgradeTotal * Mathf.Pow(100, 1)) / Mathf.Pow(100, 1) + "%" + " Per Second";
    staminaRegenUpgradeCostText.text = seenStaminaRegenUpgradeCost + " MP";
    */

    /*Simplify to floats  
    seenStaminaRegenLevel = Mathf.Round(staminaRegenArray[0] * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1);
    seenStaminaRegenUpgradeAmount = Mathf.Round(staminaRegenArray[1] * Mathf.Pow(10, 3)) / Mathf.Pow(10, 3);
    seenStaminaRegenUpgradeTotal = Mathf.Round(staminaRegenArray[2] * Mathf.Pow(10, 3)) / Mathf.Pow(10, 3);
    seenStaminaRegenUpgradeCost = Mathf.Round(staminaRegenArray[3] * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1);
    */






}