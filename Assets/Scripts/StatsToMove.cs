using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsToMove : MonoBehaviour
{

    public VariableHolder var_Script;
    public static float focus;
    public static float strength;
    public static float endurance;
    public static float agility;
    public static float vitality;



    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        strength = var_Script.Strength;
        endurance = var_Script.Endurance;
        agility = var_Script.Agility;
        vitality = var_Script.Vitality;
        var_Script = VariableHolder.FindObjectOfType<VariableHolder>();
     
    }
}
