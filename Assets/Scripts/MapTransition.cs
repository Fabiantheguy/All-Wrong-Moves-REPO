using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    public bool mapOn;
    public GameObject map;
    void Start()
    {
    }

    public void MapTransitionButton()
    {
        if(mapOn == false)
        {
            mapOn = true;
            map.SetActive(true);
        }
        else
        {
            mapOn = false;
            map.SetActive(false);
        }
    }

}
