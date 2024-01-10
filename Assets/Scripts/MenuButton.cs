using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuHolder;
    public bool screenOn;
    // Start is called before the first frame update
    void Start()
    {     
        screenOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckScreen();
    }

    public void SwitchScreen()
    {
       if(screenOn == false)
         {
            screenOn = true;
         }
        else
        {
            screenOn = false;
        }
    }

    public void CheckScreen()
    {
        if(screenOn == false)
        {
            menuHolder.SetActive(false);
        }
        else
        {
            menuHolder.SetActive(true);
        }
    }
}
