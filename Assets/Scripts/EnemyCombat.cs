using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCombat : MonoBehaviour
{
    [Header("Positions")]
    public Vector2 endPosition;
    public Vector2 startPosition;

    [Header("Game Objects")]
    public PlayerCombat playerCombat_Script;
    public CombatStats combatStats_Script;
    public GameObject enemySprite;
    public GameObject playerSprite;
    public Slider enemyHealthBar;
    public Slider enemyPunchCooldownBar;
    public Slider enemyStaminaBar;


    [Header("Enemy Health")]
    public float enemyMaxHealth;
    public float enemyHealth;
    [Header("Enemy Speed")]
    public float enemySpeed;
    public float enemyAttackSpeed;
    public float enemyCooldownSpeed;
    [Header("Enemy Endurance")]
    public float enemyStamina;
    public float enemyMaxStamina;
    public float usedStamina;
    public float enemyStaminaSpeed;
    [Header("Enemy Strength")]
    public float enemyEndurance;
    public float enemyStrength;


    public float action1SequenceFloat;
    public bool autoAttackCoolingDown;
    public bool enemyAttackRecovery;
    public bool isActing;
    public bool playerDamaged;
    public bool punchRegenning;
    public bool isAttacking;
    public bool isDead;

    public float elapsedTransformTime;
    public float percentageTransformCompleteStart;
    public float attackTransformTime;

    public float idleSpeed;
    public float idleAmount;

    public float shakeSpeed;
    public float shakeAmount;
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
    public List<GameObject> enemyAPList;

    public Animator enemyAnimator;
    private void Awake()
    {
        startPosition = transform.position;
        maxAP = 3;
    }
    void Start()
    {
        DisplayAP();
        ap = 0;
        A1Done = false;
        A2Done = false;
        A3Done = false;
        A4Done = false;
        A5Done = false;
        A6Done = false;
        hasAP = false;
        endPosition = playerCombat_Script.startPosition;
        endPosition.x += 200;
       
        enemySpeed = 2f;
        enemyPunchCooldownBar.value = 0;
        autoAttackCoolingDown = true;
        enemyMaxStamina = 30;

        attackTransformTime = 1f;
        enemyAttackRecovery = false;

        punchRegenning = true;

        enemyMaxHealth = 100;
        enemyHealth = enemyMaxHealth;

        enemyStrength = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(EnemyAutoAttack());
        enemyCooldownSpeed = enemySpeed / 5;
        enemyAttackSpeed = enemySpeed / 10;
        attackTransformTime = enemyAttackSpeed;
        EnemyAttackAnimation();
        EnemyIdle();
        Action1();
        ShakePlayer();
        EnemyDeath();
        enemyHealthBar.maxValue = enemyMaxHealth;
        enemyHealthBar.value = enemyHealth;
    }

    IEnumerator EnemyAutoAttack()
    {
        if (playerCombat_Script.isDead == false && playerCombat_Script.inAPSequenceSetup == false)
        {
            enemyStaminaBar.maxValue = enemyMaxStamina;
            enemyStaminaBar.value = enemyStamina;

            if (punchRegenning == true && playerCombat_Script.isAttacking == false)
            {
                enemyPunchCooldownBar.value += Time.deltaTime * enemyCooldownSpeed;
                enemyStamina += Time.deltaTime;
            }
            if (playerDamaged == true)
            {
                yield return new WaitForSeconds(.1f);
                playerDamaged = false;
            }


            if (enemyPunchCooldownBar.value >= 1)
            {
                //On Turn Start
                isAttacking = true;
                punchRegenning = false;
                enemyPunchCooldownBar.value = 0;
                ap = maxAP;
                usedAP = 0;
                DisplayAP();
            }
        }
    }

    public void EnemyAttackAnimation()
    {
        if (playerCombat_Script.isDead == false)
        {


            if (isActing == true)
            {
                if (enemyAttackRecovery == false)
                {
                    elapsedTransformTime += Time.deltaTime * attackTransformTime;
                    percentageTransformCompleteStart = elapsedTransformTime / attackTransformTime;
                    enemySprite.transform.position = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageTransformCompleteStart));
                    enemyAnimator.Play("Punch");
                    if (percentageTransformCompleteStart >= 1)
                    {
                        //On Hit
                        playerDamaged = true;
                        playerCombat_Script.playerHealth -= enemyStrength;
                        enemyAttackRecovery = true;
                        elapsedTransformTime = 0;
                        percentageTransformCompleteStart = 0;
                    }
                }


                if (enemyAttackRecovery == true)
                {
                    elapsedTransformTime += Time.deltaTime * enemyAttackSpeed;
                    percentageTransformCompleteStart = elapsedTransformTime / attackTransformTime;
                    enemySprite.transform.position = Vector2.Lerp(endPosition, startPosition, Mathf.SmoothStep(0, 1, percentageTransformCompleteStart));
                    if (percentageTransformCompleteStart >= 1)
                    {
                        //On Return

                        enemyAttackRecovery = false;
                        elapsedTransformTime = 0;
                        percentageTransformCompleteStart = 0;
                        apSequence += 1;
                        if (apSequence >= maxAPSequence)
                        {
                            isActing = false;
                            punchRegenning = true;
                            apSequence = 0;
                            maxAPSequence = 0;
                            isAttacking = false;
                        }

                    }
                }
            }
        }
    }
    public void EnemyIdle()
    {

        idleSpeed = 3f;    
        idleAmount = 3f;

        float x = startPosition.x + Mathf.Sin(Time.time * idleSpeed) * idleAmount;
        float y = startPosition.y;
        if(isActing == false && playerCombat_Script.enemyDamaged == false)
        {
            enemySprite.transform.position = new Vector2(x, y);
        }
        
    }
    public void ShakePlayer()
    {
        if (playerCombat_Script.isDead == false)
        {


            shakeSpeed = 30;
            shakeAmount = 5;
            float x = playerCombat_Script.startPosition.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            float y = playerCombat_Script.startPosition.y;
            if (playerDamaged == true)
            {
                playerSprite.transform.position = new Vector2(x, y);
            }
        }
    }
    public void Action1()
    {
        int action1Cost = 1;
        float action1StaminaCost = 5;
        if (isAttacking == true && isActing == false)
        {
            if(ap >= action1Cost && enemyStamina >= action1StaminaCost)
            {
                usedAP += action1Cost;
                apSequenceSetup += 1;
                ap -= action1Cost;
                enemyStamina -= action1StaminaCost;
                usedStamina += action1StaminaCost;
                maxAPSequence += 1;
                isActing = true;
            }
            else
            {
                isActing = false;
                punchRegenning = true;
                apSequence = 0;
                maxAPSequence = 0;
                isAttacking = false;
                punchRegenning = true;
            }
            DisplayAP();
        }

    }

    public void DisplayAP()
    {
        foreach (Transform ap in actionPointHolder)
        {
            Destroy(ap.gameObject);
            Debug.Log("Destroyed");
        }
        for (int i = 0; i < (maxAP - usedAP); i++)
        {
            GameObject APImage = Instantiate(apImage, actionPointHolder);
            enemyAPList.Add(APImage);
        }
        for(int i = 0; i < usedAP; i++)
        {
            GameObject APImage = Instantiate(usedAPImage, actionPointHolder);


        }

    }

public void EnemyDeath()
    {
        if(enemyHealth <= 0)
        {
            enemyHealthBar.value = 1;
            combatStats_Script.IncreaseStrength(1);
        }
    }
}
