using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatStats : MonoBehaviour
{
    public float currentFocus;
    public float currentStrength;
    public float currentEndurance;
    public static float earnedFocus;
    public static float earnedStrength;
    public static float earnedEndurance;
    public static float earnedAgility;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        currentFocus = StatsToMove.focus + earnedFocus;
        currentStrength = StatsToMove.strength + earnedStrength;
        currentEndurance = StatsToMove.endurance + earnedEndurance;
    }

    public void IncreaseFocus(float focus)
    {
        earnedFocus += 1;
    }
    public void IncreaseStrength(float strength)
    {
        earnedStrength += strength;
    }
    public void IncreaseEndurance(float endurance)
    {
        earnedEndurance += 1;
    }
}
