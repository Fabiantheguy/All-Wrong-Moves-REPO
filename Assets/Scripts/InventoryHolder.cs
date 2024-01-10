using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class InventoryHolder : MonoBehaviour
{

    public Upgrades upgrade_Script;
    public SkillsManager skills_Script;

    public void Awake()
    {
      
    }

    public void GetSkill(string skillID)
    {
        PurchaseItemRequest request = new PurchaseItemRequest();
        request.CatalogVersion = "Skills";
        request.ItemId = skillID;
        request.VirtualCurrency = "MP";
        request.Price = 0;

        PlayFabClientAPI.PurchaseItem(request, result => {
            Debug.Log(request.ItemId + " Skill Got!");
        }, error => {
            Debug.Log(error);
        });
    }

    public void GetUserSkills()
    {
        GetUserInventoryRequest request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, result =>
        {
            List<ItemInstance> ii = result.Inventory;
            foreach (ItemInstance i in ii)
            {
                skills_Script.LoadInventorySkills(i.DisplayName);
                Debug.Log(i.DisplayName.ToString());
            }
         
        }, error =>
            {
                Debug.Log(error);
            });

    }
    public void SpendCurrency(float spendamount)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "Mistakium",
            Amount = Mathf.RoundToInt(spendamount)
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractMasteryPoints, OnError);
    }
    public void GrantVirtualCurrencies(float grantamount)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "Mistakium",
            Amount = Mathf.RoundToInt(grantamount)
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantVirtualCurrencySuccess, OnError);
    }
    void OnSubtractMasteryPoints(ModifyUserVirtualCurrencyResult result)
    {

        Debug.Log("Spent MP!");
        PlayFabManager.instance.GetVirtualCurrencies();
    }
    void OnGrantVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Currency Granted!");
        PlayFabManager.instance.GetVirtualCurrencies();
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error" + error.ErrorMessage);
    }


}
