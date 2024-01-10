using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public string textToFinish;
    public float textOrder;
    public bool changingText;
    public bool playerKilled;
    public bool isTyping;
    public bool textCompleted;
    [SerializeField] bool opening = true;

    public TypeWriter typeWriter;
    // Start is called before the first frame update
    void Start()
    {
        changingText = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(chatText.text == typeWriter.writer)
        {
            textCompleted = true;
        }
        else
        {
            textCompleted = false;
        }
    }

    public void OrderText()
    {
    
      

        
        
    }

    public void SenseiTextChange()
    {
        if (textCompleted == true & opening == true)
        {


            if (typeWriter.writer == "")
            {
                typeWriter.TimeBTWChars = .15f;
                StartCoroutine(ChangeText("There is nothing more painful than the loss of a friend..."));
                textOrder += 1;
            }
            else if (chatText.text == textToFinish && textOrder == 1)
            {
                typeWriter.TimeBTWChars = .05f;
                StartCoroutine(ChangeText("WAKE UP!!!"));
                textOrder += 1;
            }

            if (chatText.text == textToFinish && textOrder == 2)
            {
                StartCoroutine(ChangeText("Testing!"));
                textOrder += 1;
            }
            if (chatText.text == textToFinish && textOrder == 3)
            {
                StartCoroutine(ChangeText("1"));
                textOrder += 1;
            }
            if (chatText.text == textToFinish && textOrder == 4)
            {
                StartCoroutine(ChangeText("2"));
                textOrder += 1;
            }
            if (chatText.text == textToFinish && textOrder == 5)
            {
                StartCoroutine(ChangeText("3"));
                textOrder += 1;
            }
        }
    }
    public void PlayerKillText()
    {
        typeWriter.TimeBTWChars = .1f;
        typeWriter.SetText();
        StartCoroutine(ChangeText("You've got a lot to learn"));
        
    }
    public void EnemyKillText()
    {

    }

    public IEnumerator ChangeText(string nextText)
    {
     
        {
            typeWriter.writer = nextText;
            textToFinish = nextText;
            changingText = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
