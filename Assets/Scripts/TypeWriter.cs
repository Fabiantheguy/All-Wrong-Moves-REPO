using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWriter : MonoBehaviour
{
	[SerializeField] TMP_Text tmpProText;
	public string writer;
	public DisplayText displayText;
	[SerializeField] private Coroutine coroutine;

	public DisplayText display;
	public AudioSource typeSound;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] string leadingChar = "";
	public bool completedTyping;
	[SerializeField] bool leadingCharBeforeDelay = false;
	public float TimeBTWChars;
	[Space(10)] [SerializeField] private bool startOnEnable = false;
	[Header("Collision-Based")]
	[SerializeField] private bool clearAtStart = false;
	[SerializeField] private bool startOnCollision = false;
	enum options { clear, complete }
	[SerializeField] options collisionExitOptions;

	// Use this for initialization
	void Awake()
	{
		TimeBTWChars = .1f;
		if (tmpProText != null)
		{
			writer = tmpProText.text;
		}
	}

	void Start()
	{
		if (!clearAtStart) return;
		if (tmpProText != null)
		{
			tmpProText.text = "";
		}
	}

    private void Update()
    {
        if(tmpProText.text.Length == leadingChar.Length)
        {
			completedTyping = true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
	{
		if (collisionExitOptions == options.complete)
		{

			if (tmpProText != null)
			{
				tmpProText.text = writer;
			}
		}
		// clear
		else
		{
			if (tmpProText != null)
			{
				tmpProText.text = "";
			}
		}
		StopAllCoroutines();
	}



	private void OnDisable()
	{
		StopAllCoroutines();
	}



	IEnumerator TypeWriterTMP(float timeBTWChars)
	{
		tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (tmpProText.text.Length >= 0)
			{
				tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
                if (tmpProText.text.EndsWith(" "))
				{
					
				}
                else
                {
					typeSound.Play();
				}
				
			}
			tmpProText.text += c;
			tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBTWChars);
		}

		if (leadingChar != "")
		{
			tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
		}
	}
	public void SetText()
	{
		if (displayText.textCompleted == true)
		{


			{
				StartCoroutine(TypeWriterTMP(TimeBTWChars));
				if (tmpProText != null)
				{
					tmpProText.text = "";

				}

			}

		}
	}
}