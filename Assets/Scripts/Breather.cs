using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Breather : MonoBehaviour
{
    [SerializeField] VariableHolder var_Script;
    [SerializeField] BreathProgress breath_Script;
    [SerializeField] Stamina stam_Script;
    [SerializeField] Upgrades upgrade_Script;
    public bool breathing;
    public float speed;
    public float baseSpeed;
    public bool canBreathe;
    public float speedIncrease;
    public bool exhaling;
    public float exhaleSpeed;
    public Button breathButton;
    public Image fillArea;
    public AudioSource breatheIn;
    public AudioSource breatheOut;
    public bool breathingOut;

    // Start is called before the first frame update
    void Start()
    {
        breathing = false;
        canBreathe = true;
        exhaling = false;
        exhaleSpeed = -.5f;
        breathingOut = false;
        baseSpeed = .1f;
    }

    // Update is called once per frame
    void Update() 
    { 
        CheckBreathing();
        CompleteBreath();
        FullExhale();
        CheckBreathingOut();
    }
    public void SwitchSpeed()
    {
       
        if (breathing == false && exhaling == false)
        {
            speed = baseSpeed;
            breathingOut = false;
            breatheIn.Play();
            breatheOut.Stop();
        }
        else
        {
            breathingOut = true;
            speed = 0f;
            breatheOut.Play();
            breatheIn.Stop();
        }
    }
    public void CheckBreathingOut()
    {
        if(breathing == false && breath_Script.breath_Bar.value != breath_Script.breath_Bar.minValue && speed < 0 && breathingOut == false)
        {
            breatheOut.Play();
            breatheIn.Stop();
            breathingOut = true;
        }
        if (breath_Script.breath_Bar.value == breath_Script.breath_Bar.minValue)
        {
            breatheOut.Stop();
        }
    }

    public void CompleteBreath()
    {
        if (breath_Script.breath_Bar.value >= 1 || stam_Script.haveStamina == false)
        {
            canBreathe = false;
        }
        else
        {
            canBreathe = true;
        }
    }


    public void CheckBreathing()
    {
        if (speed > 0 && canBreathe == true)
        {
            breathing = true;
        }
        else
        {

            breathing = false;
            speed = exhaleSpeed;
        }
    }
    public void FullExhale()
    {
        if (breath_Script.breath_Bar.value >= 1)
        {
            speed = exhaleSpeed;
            exhaling = true;
            breatheOut.Play();
            breatheIn.Stop();
            breathButton.enabled = false;

        }
        if (exhaling == true && breath_Script.breath_Bar.value <= 0)
        {
            exhaling = false;
            breathButton.enabled = true;
        }

    }

}
