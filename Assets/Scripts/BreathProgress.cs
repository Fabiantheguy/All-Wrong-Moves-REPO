using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreathProgress : MonoBehaviour
{
    [SerializeField] VariableHolder var_Script;
    public Slider breath_Bar;
    public TextMeshProUGUI focus_Text;
    public Breather breather_Script;
    [SerializeField]  float focus;
    public float speed;
    public float breathSpeedIncrease;
    public float focusMultiplier;
    private float _focusMultiplier;
    public float baseSpeedIncrease;
    public TextMeshProUGUI breath_Percentage;
    public bool letBreathFast;


    // Start is called before the first frame update
    void Start()
    {
        breath_Bar.value = 0;
        baseSpeedIncrease = .1f;
        letBreathFast = false;
    }

    // Update is called once per frame
    void Update()
    {
        speed = breather_Script.speed;
        _focusMultiplier = (float)focusMultiplier;
        speed = breather_Script.speed;
        LoadSpeed(speed);
        
        BreathTextSet(focus_Text, breath_Percentage);
    }

    public void LoadSpeed(float Speed)
    {
        breath_Bar.value += (Time.deltaTime * Speed);
    }


    public void BreathTextSet(TextMeshProUGUI breathText, TextMeshProUGUI percentageText)
    {
        breathText.text = "Focus" + "  " + focus;
        percentageText.text = $"{breath_Bar.value * 100:F0}" + "%";
    }
}