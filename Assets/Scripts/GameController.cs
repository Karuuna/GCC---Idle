using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text CurrencyText;
    public Text CookiesPerSecondText;
    public Text PurchaseAmountHandletext;
    public Slider PurchaseAmountSlider;

    public Button GetCookieButton, CookiesPerClickButton, CPS1Button, AutoclickerButton;


    public UpgradeButtonController Contr_CPS1;
    public UpgradeButtonController Contr_Autoclicker;
    public UpgradeButtonController Contr_UpgradeCookiesPerClick;
     
    public UpgradeButtonModel Model_CPS1;
    public UpgradeButtonModel Model_Autoclicker;
    public UpgradeButtonModel Model_UpgradeCookiesPerClick;

    public GameObject Vis_CPS1;
    public Transform Vis_CPS1_Prefab;


    public List<UpgradeButtonController> ButtonList;

    public GetCookieButtonView View_GetCookie;


    public float Cookies = 0;
    public float CookiesPerClick = 1;
    public float CookiesPerSecond;

    public float CookiesPerClickCostScale = 2;
    public float CPS1CostScale = 1;
    public float AutoclickerCostScale = 2;

    public float TickTime = 0.1f;
    private float _elapsedTime;
    


    public int PurchaseAmount = 1;


    // Use this for initialization
    
//      Learn how to set every button method on start by using this: https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
//  CURRENTLY BUTTONS ARE DISABLED BECAUSE PREFAB REVERT
    void Start () {
        ButtonList = new List<UpgradeButtonController>() { Contr_Autoclicker, Contr_CPS1, Contr_UpgradeCookiesPerClick};
        Button btn1 = GetCookieButton.GetComponent<Button>();
        Button btn2 = CookiesPerClickButton.GetComponent<Button>();
        Button btn3 = CPS1Button.GetComponent<Button>();
        Button btn4 = AutoclickerButton.GetComponent<Button>();

        btn1.onClick.AddListener(GetCookie);
        btn2.onClick.AddListener(BuyCookiesPerClick);
        btn3.onClick.AddListener(BuyCPS1);
        btn4.onClick.AddListener(BuyAutoclicker);

        Contr_CPS1.View.Label.text = "Cookies per Second";
        Contr_Autoclicker.View.Label.text = "Autoclicker";
        Contr_UpgradeCookiesPerClick.View.Label.text = "Cookies per Click";

        Contr_UpgradeCookiesPerClick.Model.CostScaleFactor = CookiesPerClickCostScale;
        Contr_CPS1.Model.CostScaleFactor = CPS1CostScale;
        Contr_Autoclicker.Model.CostScaleFactor = AutoclickerCostScale;


        CurrencyText.text = Cookies.ToString();
    }

    // Update is called once per frame  
    void Update () {

       CPS();
  
    }

            //
        // BUYING / UPGRADING
            //

private void PurchaseableCheck() //Make sure that CheckPurchaseable also sets the cost text 
    {
        foreach (UpgradeButtonController button in ButtonList)
        { button.CheckPurchaseable(Cookies); }
    }

    private bool SpendCookies(float price)
    {
        if (Cookies >= price)
        {
            Cookies = Cookies - price;
            CurrencyText.text = Cookies.ToString();

            PurchaseableCheck();

            return true;
        }
        else return false;
    }

    
    public void BuyCookiesPerClick() //Missing: Visualisation of cookies per click
    {
        BuyStuff(Model_UpgradeCookiesPerClick, Contr_UpgradeCookiesPerClick);
        CookiesPerClick = Model_UpgradeCookiesPerClick.Amount +1;
    }

    public void BuyCPS1()
    {
        BuyStuff(Model_CPS1, Contr_CPS1);
        
        Vector2 OffsetPos = (Vector2)Vis_CPS1.transform.position + UnityEngine.Random.insideUnitCircle * 5;
        for (int i = 0; i < PurchaseAmount; i++)
        {
            Instantiate(Vis_CPS1_Prefab, OffsetPos, Quaternion.identity, Vis_CPS1.transform);
            
        }

    }

    public void BuyAutoclicker()
    {
        BuyStuff(Model_Autoclicker, Contr_Autoclicker);
    }

    public void BuyStuff(UpgradeButtonModel model, UpgradeButtonController controller)
    {
        if (SpendCookies(model.Cost))
        {
            controller.Purchase(PurchaseAmount);

            UpdateCookiesPerSecond();
        } //Cost for purchasing more than 1 is currently not being adjusted correctly 
        else
        { controller.ShowErrorCookiesMissing((model.Cost - Cookies)); }
    }
    /*
    public void BuyStuff(UpgradeButtonModel model, UpgradeButtonController controller, GameObject vis)
    {
        if (SpendCookies(model.Cost))
        {
            controller.Purchase(PurchaseAmount);
            controller.SpawnBuilding(vis);
            UpdateCookiesPerSecond();
        } //Cost for purchasing more than 1 is currently not being adjusted correctly 
        else
        { controller.ShowErrorCookiesMissing((model.Cost - Cookies)); }
    }*/


    //
    // COOKIE GENERATION
    //

    public void GetCookie()
    {
        Cookies = Cookies + CookiesPerClick;
        CurrencyText.text = Cookies.ToString();

        PurchaseableCheck();

        View_GetCookie.ParticleCookie(CookiesPerClick);
    }

    public void CPS() //Cookies per second
    {
        _elapsedTime = _elapsedTime + Time.deltaTime;
        if (_elapsedTime >= TickTime)
        {
            if (Model_CPS1.Amount > 0)
            {
                Cookies = (Cookies + TickTime * Model_CPS1.Amount);

                CurrencyText.text = Cookies.ToString();
            }
            if (Model_Autoclicker.Amount > 0)
            {
                Cookies = (Cookies + TickTime * CookiesPerClick * Model_Autoclicker.Amount);
                CurrencyText.text = Cookies.ToString();
            }
            PurchaseableCheck();
            _elapsedTime = 0;
        }
    } 
    
     private float UpdateCookiesPerSecond()
    {
        CookiesPerSecond = TickTime * CookiesPerClick * Model_Autoclicker.Amount + TickTime * Model_CPS1.Amount;
        CookiesPerSecondText.text = CookiesPerSecond.ToString() + " C/s";
        return CookiesPerSecond;
    }

    public void UpdatePurchaseAmount()
    {
        PurchaseAmount = Convert.ToInt32(PurchaseAmountSlider.value);
        PurchaseAmountHandletext.text = PurchaseAmount.ToString();

        foreach (UpgradeButtonController button in ButtonList)
        { button.UpdateCost(PurchaseAmount); }

        PurchaseableCheck();

    }


}



/* 
 
To do:
- Visualisation of Upgrade levels (spawn more prefab thingies)
    Import asset packs
    https://kenney.nl/assets 
    
    

    Model - holding information
    View - everyting visual
    Controller - manipulate data
     
*/
