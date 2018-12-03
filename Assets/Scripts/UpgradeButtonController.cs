using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonController : MonoBehaviour {
    public UpgradeButtonModel Model;
    public UpgradeButtonView View;

  
    public void Purchase(int amount)
    {
        Model.Amount = Model.Amount + amount;
        UpdateCost(amount);
        View.UpdateAmount(Model.Amount);
        View.AmountIncrease(amount);
        Debug.Log("Purchase successful, total owned: " + Model.Amount.ToString());
    }
/*
    public void   SpawnBuilding(GameObject vis)
    {
        Transform Clone = Instantiate(AmountIncreaseTextPrefab, , Quaternion.identity, Amount.transform);
    }
    */

    public void ShowErrorCookiesMissing(float missing)
    {
        View.ShowErrorCookiesMissing(missing);
    }

    public void CheckPurchaseable(float cookies)
    {
        if (Model.Cost <= cookies)
        {
            View.SetCostColour(ButtonState.Purchaseable);
        }
        else
        {
            View.SetCostColour(ButtonState.NotPurchaseable);
        }

    }


    //Cost math calc could be in model

    public void UpdateCost(int amount)
    {
        Model.Cost = 0;
        int MinNextPurchaseAmount = (Model.Amount + 1);
        int MaxnextPurchaseAmount = (Model.Amount + amount);

        for (int i = MinNextPurchaseAmount;  i <= MaxnextPurchaseAmount; i++)
        {
            Model.Cost += i * 10 * Model.CostScaleFactor;
        }
        //10 20 30 40 50 ...
        // Model.Cost = (Model.Amount + amount ) * 10 + amount * 10 + 10; //Add Model.CostScaleFactor
        View.UpdateCost(Model.Cost);
    }


}
