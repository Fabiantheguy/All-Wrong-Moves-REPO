using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{

    public static GameManager gameManager { get; private set; }
    
    public VariableHolder variables = new VariableHolder(1,1,1,1,1,1,1,1,1,1,1,1);
    private void Start()
    {
       
    }
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
    private void Update()
    {
       
        
    }

}