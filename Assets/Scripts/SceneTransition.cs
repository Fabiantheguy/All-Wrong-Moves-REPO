using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    
    public void HomeTransition()
    {
        SceneManager.LoadScene("HomeScene");
       
    }

    public void CombatTransition()
    {
        SceneManager.LoadScene("CombatScene");
    
    }
}
