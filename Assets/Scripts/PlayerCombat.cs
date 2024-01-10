using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Positions")]
    public Vector2 endPosition;
    public Vector2 startPosition;

    [Header("Game Objects")]
    public EnemyCombat enemyCombat_Script;
    public CombatStats combatStats_Script;
    public DisplayText displayText_Script;
    public GameObject playerSprite;
    public GameObject enemySprite;
    public Slider playerHealthBar;
    public Slider playerActCooldownBar;
    public Slider playerStaminaBar;
    public Slider playerStaminaMask;

    [Header("Player Health")]
    public float playerMaxHealth;
    public float playerHealth;
    [Header("Player Speed")]
    public float playerSpeed;
    public float playerAttackSpeed;
    public float playerCooldownSpeed;
    [Header("Player Endurance")]
    public float playerStamina;
    public float playerMaxStamina;
    public float usedStamina;
    public float playerStaminaSpeed;
    [Header("Player Strength")]
    public float playerEndurance;
    public float playerStrength;

    [Header("Player XP")]
    public float staminaToXP;
    public float staminaToXPReq;

    public bool autoAttackCoolingDown;
    public bool playerAttackRecovery;
    public bool isActing;
    public bool enemyDamaged;
    public bool punchRegenning;
    public bool isAttacking;
    public bool isDead;

    private float elapsedTransformTime;
    private float percentageTransformCompleteStart;
    private float attackTransformTime;

    private float idleSpeed;
    private float idleAmount;

    private float shakeSpeed;
    private float shakeAmount;
    [Header("Action Points")]
    private bool A1Done;
    private bool A2Done;
    private bool A3Done;
    private bool A4Done;
    private bool A5Done;
    private bool A6Done;

    public bool hasAP;
    public bool currentlyActing;
    public int ap;
    public int maxAP;
    public int usedAP;
    public bool inAPSequenceSetup;
    public float apSequenceSetup = 0;
    public float maxAPSequenceSetup = 0;
    public float apSequence = 0;
    public float maxAPSequence = 2;
    public Transform actionPointHolder;
    public GameObject apImage;
    public GameObject usedAPImage;
    public TextMeshProUGUI action1SequenceText;
    public TextMeshProUGUI action2SequenceText;
    public List<GameObject> playerAPList;

    public GameObject actionsHolder;
    public Animator playerAnimator;
    private void Awake()
    {
        startPosition = transform.position;
        maxAP = 3;
    }
    void Start()
    {
        SetScreen(false);
        DisplayAP();
        ap = 0;
        A1Done = false;
        A2Done = false;
        A3Done = false;
        A4Done = false;
        A5Done = false;
        A6Done = false;
        hasAP = false;
        endPosition = enemyCombat_Script.startPosition;
        endPosition.x -= 200;
        startPosition = transform.position;
        playerSpeed = 1;
        playerActCooldownBar.value = 0;
        playerStaminaMask.value = 0;
        playerStaminaBar.value = 0;
        autoAttackCoolingDown = true;

        attackTransformTime = 1f;
        playerAttackRecovery = false;
        isAttacking = false;
        punchRegenning = true;
        isActing = true;
        staminaToXPReq = 5f;

        playerMaxHealth = 100;
        playerHealth = playerMaxHealth;



    }

    // Update is called once per frame
    void Update()
    {
        playerStaminaBar.maxValue = playerMaxStamina;
        playerStaminaBar.value = playerStamina;
        playerStaminaMask.maxValue = playerStaminaBar.maxValue;
        playerMaxStamina = playerEndurance * 5 + 100;
        PlayerActionGain();
        PlayerDeath();
        attackTransformTime = playerAttackSpeed;
        PlayerAttackAnimation();
        PlayerIdle();
        StartCoroutine(ShakeEnemy());
        OrderAttacks();
        playerStrength = combatStats_Script.currentStrength;
        playerEndurance = combatStats_Script.currentStrength;
        playerHealthBar.maxValue = playerMaxHealth;
        playerHealthBar.value = playerHealth;
        maxAPSequence = apSequenceSetup;
    }

    public void PlayerActionGain()
    {
        if (isDead == false && inAPSequenceSetup == false)
        {

            playerCooldownSpeed = playerSpeed / 5;
            playerStaminaSpeed = playerEndurance / 5 + 1;
            playerAttackSpeed = playerSpeed / 10;
            if (punchRegenning == true && enemyCombat_Script.isAttacking == false)
            {
                playerActCooldownBar.value += Time.deltaTime * playerCooldownSpeed;
                playerStamina += Time.deltaTime * playerStaminaSpeed;
                staminaToXP += Time.deltaTime * playerStaminaSpeed;
            }
            if (staminaToXP >= staminaToXPReq)
            {
                staminaToXP = 0;
                combatStats_Script.IncreaseEndurance(1);
            }

            if (playerActCooldownBar.value == 1)
            {
                //On Turn Start
                StartSequenceSetup();
                SetScreen(true);
                playerActCooldownBar.value = 0;
                ap = maxAP;
                usedAP = 0;
                DisplayAP();
            }
        }
    }

    public void PlayerAttackAnimation()
    {
        if (isDead == false)
        {
            if (isActing == false)
            {
                if (playerAttackRecovery == false)
                {
                    currentlyActing = true;
                    elapsedTransformTime += Time.deltaTime * attackTransformTime;
                    percentageTransformCompleteStart = elapsedTransformTime / attackTransformTime;
                    playerSprite.transform.position = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageTransformCompleteStart));
                    playerAnimator.Play("Punch");
                    if (percentageTransformCompleteStart >= 1)
                    {
                        //On Hit
                        enemyDamaged = true;
                        enemyCombat_Script.enemyHealth -= playerStrength;
                        playerAttackRecovery = true;
                        
                        elapsedTransformTime = 0;
                        percentageTransformCompleteStart = 0;

                    }
                }


                if (playerAttackRecovery == true && inAPSequenceSetup == true)
                {
                    elapsedTransformTime += Time.deltaTime * playerAttackSpeed;
                    percentageTransformCompleteStart = elapsedTransformTime / attackTransformTime;
                    playerSprite.transform.position = Vector2.Lerp(endPosition, startPosition, Mathf.SmoothStep(0, 1, percentageTransformCompleteStart));
                    if (percentageTransformCompleteStart >= 1)
                    {
                        //On Return
                        isAttacking = false;
                        playerAttackRecovery = false;
                        elapsedTransformTime = 0;
                        percentageTransformCompleteStart = 0;
                        isActing = true;
                        punchRegenning = true;
                        apSequence += 1;
                        currentlyActing = false;
                        DisplayAP();

                        if (apSequence >= maxAPSequence)
                        {
                            action1SequenceText.text = "";
                            inAPSequenceSetup = false;
                            A1Done = false;
                            A2Done = false;
                            A3Done = false;
                            A4Done = false;
                            A5Done = false;
                            A6Done = false;
                        }
                    }
                }
            }
        }
    }
    public void PlayerIdle()
    {

        idleSpeed = 3f;
        idleAmount = 3f;

        float x = startPosition.x + Mathf.Sin(Time.time * idleSpeed) * idleAmount;
        float y = startPosition.y;
        if (isActing == true && enemyCombat_Script.playerDamaged == false)
        {
            playerSprite.transform.position = new Vector2(x, y);
        }

    }
    IEnumerator ShakeEnemy()
    {
        shakeSpeed = 30;
        shakeAmount = 5;
        float x = enemyCombat_Script.startPosition.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        float y = enemyCombat_Script.startPosition.y;
        if (enemyDamaged == true)
        {
            enemySprite.transform.position = new Vector2(x, y);
        }
        if (enemyDamaged == true)
        {
            yield return new WaitForSeconds(.1f);
            enemyDamaged = false;
        }
    }

    public void PlayerDeath()
    {
        if (playerHealth <= 0 && isDead == false)
        {
            displayText_Script.PlayerKillText();
            isDead = true;
        }
    }
    //Trigger when bar fills
    public void StartSequenceSetup()
    {
        playerStaminaMask.value = playerStaminaBar.value;
        hasAP = true;
        inAPSequenceSetup = true;
        enemyCombat_Script.enemyCooldownSpeed = 0;
        playerCooldownSpeed = 0;
        apSequenceSetup = 0;
        apSequence = 0;
        maxAPSequenceSetup = maxAP;
        idleSpeed = 0;
        idleAmount = 0;
        enemyCombat_Script.idleSpeed = 0;
        enemyCombat_Script.idleAmount = 0;
    }

    public void Action1()
    {

        int action1Cost = 1;
        float action1StaminaCost = 3;
        if (ap >= action1Cost && isActing == true && playerStamina >= action1StaminaCost)
        {
            usedAP += action1Cost;
            apSequenceSetup += 1;
            ap -= action1Cost;
            playerStamina -= action1StaminaCost;
            usedStamina += action1StaminaCost;

            if (action1SequenceText.text == "")
            {
                action1SequenceText.text += apSequenceSetup;
            }
            else
            {
                action1SequenceText.text += "," + apSequenceSetup;
            }
            DisplayAP();
        }

    }
    public void Action2()
    {
        int action2Cost = 0;
        if (ap >= action2Cost && isActing == true)
        {
            apSequenceSetup += 1;
            ap -= action2Cost;
            isAttacking = true;
            punchRegenning = false;
            isActing = false;
        }

    }
    public void Confirm()
    {
        playerStaminaMask.value = 0;
        usedStamina = 0;
        if (action1SequenceText.text.Contains("1"))
        {
            isActing = false;
            A1Done = true;
        }
        if (ap == maxAP)
        {
            inAPSequenceSetup = false;
        }
        SetScreen(false);
    }
    public void Revert()
    {
        A1Done = false;
        A2Done = false;
        A3Done = false;
        A4Done = false;
        A5Done = false;
        A6Done = false;
        apSequenceSetup = 0;
        ap = maxAP;
        action1SequenceText.text = "";
        playerStamina += usedStamina;
        usedStamina = 0;
        usedAP = 0;
        DisplayAP();
    }
    public void OrderAttacks()
    {
        if (A1Done == true && apSequence == maxAPSequence)
        {

            hasAP = false;

            action1SequenceText.text = "";
            inAPSequenceSetup = false;

        }
        if (A1Done == true && apSequence != maxAPSequence && currentlyActing == false && A2Done == false && isActing == true)
        {
            isActing = false;
            A2Done = true;
        }
        if (A2Done == true && apSequence != maxAPSequence && currentlyActing == false && A3Done == false && isActing == true)
        {
            isActing = false;
            A3Done = true;
        }
        if (A3Done == true && apSequence != maxAPSequence && currentlyActing == false && A4Done == false)
        {
            isActing = false;
            A4Done = true;
        }
        if (A4Done == true && apSequence != maxAPSequence && currentlyActing == false && A5Done == false)
        {
            isActing = false;
            A5Done = true;
        }
        if (A5Done == true && apSequence != maxAPSequence && currentlyActing == false && A6Done == false)
        {
            isActing = false;
            A6Done = true;
        }
    }
    public void DisplayAP()
    {       
        foreach(Transform ap in actionPointHolder)
        {
            Destroy(ap.gameObject);
            Debug.Log("Destroyed");
        }
        for (int i = 0; i < (maxAP - usedAP); i++)
        {
            GameObject APImage = Instantiate(apImage, actionPointHolder);
            playerAPList.Add(APImage);
        }
        for (int i = 0; i < usedAP; i++)
        {
            GameObject UsedAPImage = Instantiate(usedAPImage, actionPointHolder);
            playerAPList.Add(UsedAPImage);
        }
    }

    public void SetScreen(bool State)
    {
        actionsHolder.SetActive(State);
    }
}
