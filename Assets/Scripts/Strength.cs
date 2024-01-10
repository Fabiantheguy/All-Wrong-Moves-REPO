using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Strength : MonoBehaviour
{
    [SerializeField] VariableHolder var_Script;
    [SerializeField] Stamina stam_Script;
    [SerializeField] Upgrades upgrade_Script;
    [SerializeField] InventoryHolder inventory_Script;
    public Slider trainingBagHealthSlider;
    public Slider cooldown;
    private float rockRegenSpeed;
    public AudioSource bang;
    public AudioSource destroy;
    private bool regening;
    private bool coolingDown;

    private float baseCooldownSpeed;
    public float totalCooldownSpeed;

    public float strength;
    private float strengthIncrease;
    public float damageXPAmount;
    public float destroyXPAmount;
    private float multXP;
    private float addXP;
    public float totalDamageXPGain;
    public float totalDestroyXPGain;
    private bool canMultXP;
    private bool canAddXP;
    public float trainingBagHealth;
    public float maxTrainingBagHealth;
    public float strengthXP;
    public float strengthXPReq;
    private bool leveling;
    public float masteryPointGain;
    // Start is called before the first frame update
    void Start()
    {
        masteryPointGain = 1;
        rockRegenSpeed = .5f;
        baseCooldownSpeed = .1f;
        maxTrainingBagHealth = 10;
        trainingBagHealth = maxTrainingBagHealth;
        cooldown.value = cooldown.maxValue;
        regening = false;
        damageXPAmount = 1;
        destroyXPAmount = 10;
        canAddXP = true;
        canMultXP = false;
    }

    // Update is called once per frame
    void Update()
    {
        totalCooldownSpeed = baseCooldownSpeed;
        strengthXP = var_Script.StrengthXP;
        strengthXPReq = var_Script.StrengthXPReq;
        trainingBagHealthSlider.value = trainingBagHealth;
        trainingBagHealthSlider.maxValue = maxTrainingBagHealth;
        strength = var_Script.Strength;
        strengthIncrease = var_Script.StrengthIncrease;
        totalDamageXPGain = (damageXPAmount + addXP) * multXP;
        totalDestroyXPGain = (destroyXPAmount + addXP) * multXP;
        CheckRock();
        CoolDown();
        StartCoroutine(StrengthUp());
    }
    public void DamageRock()
    {
        if(regening == false && stam_Script.haveStamina == true && var_Script.Stamina >= 2 && coolingDown == false)
        {
            var_Script.GainStrengthXP(totalDamageXPGain);
            DestroyRock();
            trainingBagHealth -= strength;
            cooldown.value = 0;
            coolingDown = true;
            bang.Play();
            var_Script.Stamina -= 2;
        }    
    }

    public void CoolDown()
    {
        if (coolingDown == true && cooldown.value < 1)
        {
            cooldown.value += (Time.deltaTime * totalCooldownSpeed);
        }
        if (cooldown.value >= 1)
        {
            coolingDown = false;
        }
    }

    public void DestroyRock()
    {
        if(trainingBagHealthSlider.value <= strength)
        {
            destroy.Play();
            inventory_Script.GrantVirtualCurrencies(masteryPointGain);

            regening = true;
            var_Script.GainStrengthXP(totalDestroyXPGain);
        }
    }

    public void CheckRock()
    {
        if (regening == true)
        {
            trainingBagHealth += (Time.deltaTime * rockRegenSpeed);
        }
        if (trainingBagHealth >= maxTrainingBagHealth && regening == true)
        {
            trainingBagHealthSlider.value = trainingBagHealthSlider.maxValue;
            regening = false;
        }
        if (trainingBagHealth < 0)
        {
            trainingBagHealth = 0;
        }
    }

    IEnumerator StrengthUp()
    {
        if (strengthXP >= strengthXPReq && leveling == false)
        {
            leveling = true;
            var_Script.SetStrengthXP(strengthXP - strengthXPReq);
            var_Script.AddStrengthXPReq(5);
            var_Script.AddStrengthLevels(1);
            var_Script.StrengthIncreaseChange(2);
            leveling = false;
            yield return new WaitForSeconds(.01f);
        }
    }
}
