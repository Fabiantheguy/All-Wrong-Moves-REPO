using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class PlayerStats
    {
    public float agility { get; set; }
    public float endurance { get; set; }
    public float endurancexp { get; set; }
    public float endurancexpreq { get; set; }
    public float strength { get; set; }
    public float strengthxp { get; set; }
    public float strengthxpreq { get; set; }
    public float vitality { get; set; }
    public float wlmasterypoints { get; set; }
    public PlayerStats(float agility, float endurance, float endurancexp, 
        float endurancexpreq,float strength, float strengthxp,
        float strengthxpreq, float vitality, float wlmasterypoints)
        {
            this.agility = agility;
            this.endurance = endurance;
            this.endurancexp = endurancexp;
            this.endurancexpreq = endurancexpreq;
   
            this.strength = strength;
            this.strengthxp = strengthxp;
            this.strengthxpreq = strengthxpreq;
            this.vitality = vitality;
            this.wlmasterypoints = wlmasterypoints;
        }
    }


public class VariableHolder : MonoBehaviour
{

   
    //Fields
    [SerializeField] PlayFabManager playfab_Script;
    [SerializeField] Upgrades upgrade_Script;


    [SerializeField] float stamina;
    [SerializeField] float maxStamina;
    [SerializeField] float endurance;
    [SerializeField] float enduranceXP;
    [SerializeField] float enduranceXPReq;
   
    [SerializeField] float staminaIncrease;

    [SerializeField] float strength;
    [SerializeField] float strengthXP;
    [SerializeField] float strengthXPReq;
    [SerializeField] float strengthIncrease;

    [SerializeField] float agility;

    [SerializeField] float vitality;

    [SerializeField] float wlMasteryPoints;

    public PlayerStats ReturnClass()
    {
        return new PlayerStats(agility, endurance, enduranceXP, enduranceXPReq, strength, strengthXP, strengthXPReq, vitality, wlMasteryPoints);
    }

    // Start is called before the first frame update
    void Start()
    {
       
        
        enduranceXPReq = 5;
        enduranceXP = 1;
        endurance = 1;
        staminaIncrease = .2f;

        strengthXPReq = 5;
        strengthXP = 1;
        strength = 1;
        strengthIncrease = .005f;

        agility = 1;
        vitality = 1;
    }

    void Update()
    {  
    }
    //Properties

    //Endurance
    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    public float MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }
    public float EnduranceXP
    {
        get { return enduranceXP; }
        set { enduranceXP = value; }
    }
    public float Endurance
    {
        get { return endurance; }
        set { endurance = value; }
    }
    public float EnduranceXPReq
    {
        get { return enduranceXPReq; }
        set { enduranceXPReq = value; }
    }
    public float StaminaIncrease
    {
        get { return staminaIncrease; }
        set { staminaIncrease = value; }
    }

    //Strength
    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    public float StrengthXP
    {
        get { return strengthXP; }
        set { strengthXP = value; }
    }
    public float StrengthXPReq
    {
        get { return strengthXPReq; }
        set { strengthXPReq = value; }
    }
    public float StrengthIncrease
    {
        get { return strengthIncrease; }
        set { strengthIncrease = value; }
    }

    //Speed
    public float Agility
    {
        get { return agility; }
        set { agility = value; }
    }

    //Vitality
    public float Vitality
    {
        get { return vitality; }
        set { vitality = value; }
    }
    //Mastery Points
    public float WLMasteryPoints
    {
        get { return wlMasteryPoints; }
        set { wlMasteryPoints = value; }
    }

    //Constructor
    public VariableHolder(float curbreathspeed, float curstamina, float curmaxstamina, float curenduranceXP, float curenduranceXPreq, float curendurance, float curstaminaincrease, float curstrength,
    float curstrengthXP, float curstrengthXPreq, float curstrengthincrease, float curwlmasterypoints)
    {

        curstamina = stamina;
        curmaxstamina = maxStamina;
        curenduranceXP = enduranceXP;
        curenduranceXPreq = enduranceXPReq;
        curendurance = endurance;
        curstaminaincrease = staminaIncrease;

        curstrength = strength;
        curstrengthXP = strengthXP;
        curstrengthXPreq = strengthXPReq;
        curstrengthincrease = strengthIncrease;

        curwlmasterypoints = wlMasteryPoints;
    }


    //Endurance
    public void AddMaxStamina(float addAmount)
    {
        maxStamina += addAmount;
    }
    public void AddEnduranceXPReq(float addAmount)
    {
        enduranceXPReq += addAmount;
    }
    public void SetEnduranceXP(float setAmount)
    {
        enduranceXP = setAmount;
    }
    public void AddEndurance(float addAmount)
    {
        endurance += addAmount;
    }

    public void StaminaDrain(float DrainSpeed)
    {
        stamina -= (Time.deltaTime * DrainSpeed);
    }
    public void StaminaRegen(float RegenSpeed)
    {
        stamina += (Time.deltaTime * RegenSpeed);
    }
    public void StaminaCap()
    {
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
        }
    }
    public void GainEnduranceXP(float XPAmount)
    {
        enduranceXP += XPAmount;
    }
    public void StaminaIncreaseChange(float ChangeAmount)
    {
        if (endurance % 5 == 0)
        {
            staminaIncrease += ChangeAmount;
        }
    }

    //Strength
    public void AddStrengthXPReq(float addAmount)
    {
        strengthXPReq += addAmount;
    }
    public void SetStrengthXP(float setAmount)
    {
        strengthXP = setAmount;
    }
    public void AddStrengthLevels(float addAmount)
    {
        strength += addAmount;
    }

    public void GainStrengthXP(float XPAmount)
    {
        strengthXP += XPAmount;
    }
    public void StrengthIncreaseChange(float ChangeAmount)
    {
        if (strength % 5 == 0)
        {
            strengthIncrease += ChangeAmount;
        }
    }


    public void SetStats(PlayerStats playerStats)
    {
        Agility = playerStats.agility + CombatStats.earnedAgility;
        Endurance = playerStats.endurance + CombatStats.earnedEndurance;
        EnduranceXP = playerStats.endurancexp;
        EnduranceXPReq = playerStats.endurancexpreq;
        Strength = playerStats.strength + CombatStats.earnedStrength;
        StrengthXP = playerStats.strengthxp;
        StrengthXPReq = playerStats.strengthxpreq;
        Vitality = playerStats.vitality;
    }
    public void SetAgility(float SaveAgility)
    {
        
    }
    public void SetVitality(float SaveVitality)
    {
      
    }

    public void SetEndurance(string SaveEndurance, string SaveEnduranceXP, string SaveEnduranceXPReq)
    {

        Endurance = float.Parse(SaveEndurance);
        EnduranceXP = float.Parse(SaveEnduranceXP);
        EnduranceXPReq = float.Parse(SaveEnduranceXPReq);
    }

    public void GainWLMasteryPoints(float GainAmount)
    {
        wlMasteryPoints += GainAmount;
    }
    public void SpendMasteryPoints(float SpendAmount)
    {
        if (wlMasteryPoints >= SpendAmount)
            wlMasteryPoints -= SpendAmount;
    }
    public IEnumerator LoadAfterStats()
    {
        yield return new WaitForSeconds(3);
        stamina = maxStamina;
    }
}

