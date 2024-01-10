using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayCombatStats : MonoBehaviour
{
    public float currentFocus;
    public float currentStrength;
    public float currentEndurance;
    public float currentAgility;
    public float currentVitality;
    public static float earnedFocus;
    public static float earnedStrength;
    public static float earnedEndurance;
    public static float earnedAgility;
    public static float earnedVitality;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        currentFocus = StatsToMove.focus;
        currentStrength = StatsToMove.strength;
        currentEndurance = StatsToMove.endurance;
        currentAgility = StatsToMove.agility;
        currentVitality = StatsToMove.vitality;
    }

    public void IncreaseFocus(float Focus)
    {
        earnedFocus += Focus;
    }
    public void IncreaseStrength(float Strength)
    {
        earnedStrength += Strength;
    }
    public void IncreaseEndurance(float Endurance)
    {
        earnedEndurance += Endurance;
    }
}
