using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class PlayFabManager : MonoBehaviour
{
    public VariableHolder[] variableHolders;
    public SkillsLoadout[] skillsLoadouts;

    private string ID;
    private bool emailLogin;

    private Scene homeScene;
    [Header("Start UI")]
    public GameObject startScreen;
    public GameObject loadScreen;
    public GameObject everything;
    public string errorForText;
    private bool firstMasteryPointCall = true;
    [Header("Start UI")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI passwordText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    [Header("Scripts")]
    public static PlayFabManager instance;
    public VariableHolder var_Script;
    public SkillsManager skillManager_Script;
    public PlayFabManager playfab_Script;
    public InventoryHolder inventory_Script;
    [Header("Save")]
    public float saveTimer;
    public float savedMasteryPoints;
    public static bool loggedIn = false;
    // Start is called before the first frame update
    void Awake()
    {
        Login();
        System.Array.Resize(ref variableHolders, variableHolders.Length + 1);
        variableHolders[0] = VariableHolder.FindObjectOfType<VariableHolder>();
        System.Array.Resize(ref skillsLoadouts, skillsLoadouts.Length + 1);
        skillsLoadouts[0] = SkillsLoadout.FindObjectOfType<SkillsLoadout>();
        instance = this;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        homeScene = SceneManager.GetActiveScene();
        ChangeStartScreen();
    }

    public void ChangeStartScreen()
    {
        if (loggedIn == false)
        {
            
            startScreen.SetActive(true);
            
            Debug.Log("First Login This Session");
        }
        else
        {
           
            Destroy(startScreen);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!string.Equals(scene.path, this.homeScene.path)) return;
        if(loggedIn == true)
        {
            GetVirtualCurrencies();
            LoadStats();
            Debug.Log("Re-Initializing!");
        }
        
    }

    private void Update()
    { 
        if (loggedIn == true)
        { 
            saveTimer += Time.deltaTime * (1 / Time.timeScale);
        }
        SetMessageText();
        SaveStats();
        
    }

    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void GrabData(string myPlayFabID)
    {
        var request = new GetAccountInfoRequest
        {
            PlayFabId = myPlayFabID
        };
        PlayFabClientAPI.GetAccountInfo(request, GetAccountInfoResult, OnError);
    }
    public void LinkAccount()
    {
        var request = new LinkCustomIDRequest
        {
            CustomId = ID
        };
        PlayFabClientAPI.LinkCustomID(request, OnLinkSuccess, OnError);
    }
    public void LoginButton()
    {

        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
            
        };
        emailLogin = true;
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "BC7E7"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    //Login Device
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true

        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }



    public void LoadStats()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }
    public void SaveStats()
    {
        
        if (saveTimer >= 3f && loggedIn == true)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    {"Player Stats", JsonConvert.SerializeObject(variableHolders[0].ReturnClass())},
                    {"Player Skills Loadout", JsonConvert.SerializeObject(skillsLoadouts[0].ReturnClass())},
                }
               
            };
            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
            saveTimer = 0;
        }


    }
    void OnDataReceived(GetUserDataResult result)
    { 

        Debug.Log("Received user data!");
        if (result.Data != null)
        {
            PlayerStats playerStats = JsonConvert.DeserializeObject<PlayerStats>(result.Data["Player Stats"].Value);
            SkillsLoadoutStats skillsLoadoutstats = JsonConvert.DeserializeObject<SkillsLoadoutStats>(result.Data["Player Skills Loadout"].Value);
            for (int i = 0; i < variableHolders.Length; i++)
            {
                variableHolders[i].SetStats(playerStats);
            };
            for (int i = 0; i < skillsLoadouts.Length; i++)
            {
                skillsLoadouts[i].SetSkillSlots(skillsLoadoutstats);
            };
        }
        else
        {
            Debug.Log("Player data not complete");
        }
    }


    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        if (firstMasteryPointCall == true)
        {
            savedMasteryPoints = result.VirtualCurrency["MP"];
            var_Script.WLMasteryPoints = savedMasteryPoints;
            firstMasteryPointCall = false;
        }     else 
        {
            savedMasteryPoints = result.VirtualCurrency["MP"];
        }
    }



    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send");
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        if (loggedIn == false)
        {
            loggedIn = true;
            Invoke("ChangeStartScreen", .5f);
            messageText.text = "Registered and logged in!";
        }
    }

    void GetAccountInfoResult(GetAccountInfoResult result)
    {
        //Character ID
        Debug.Log(result.AccountInfo.PlayFabId);
        ID = result.AccountInfo.PlayFabId;
    }
    void OnLinkSuccess(LinkCustomIDResult result)
    {
    }
    void OnLoginSuccess(LoginResult result)
    {
        StartCoroutine(var_Script.LoadAfterStats());
        StartCoroutine(StartUp());
        Debug.Log("Succesful login/account create!");       
        if(emailLogin == true)
        {
            GrabData(result.PlayFabId);
            LinkAccount();
            emailLogin = false;
        }
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        passwordText.text = "Password reset email was sent!";
    }
  
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        errorForText = error.ToString();
    }
    
    

    IEnumerator StartUp()
    {
        yield return new WaitForSeconds(.1f);
        LoadStats();
        GetVirtualCurrencies();
        inventory_Script.GetUserSkills();
        loggedIn = true;
    }
    
    public void SetMessageText()
    {
        if (loggedIn == false)
        {


            if (errorForText.Contains("Email: Email address is not valid"))
            {
                messageText.text = "Email address is invalid";
            }
            if (errorForText.Contains("Password: Password must be between 6 and 100 characters"))
            {
                messageText.text = "Password must have 7 or more characters";
            }
            if (errorForText.Contains("Email: Email address already exists"))
            {
                messageText.text = "This email is already attached to an account";
            }
            if (errorForText.Contains("Invalid email address or password"))
            {
                messageText.text = "Invalid email address or password";
            }
        }
    }
}
