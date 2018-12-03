using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    Purchaseable,
    NotPurchaseable
}

public class UpgradeButtonView : MonoBehaviour {
    public Text Amount;
    public Text AmountIncreaseText;
    public Text Cost;
    public Text ErrorCookiesMissingText;
    public Text Label;

    public Transform AmountIncreaseTextPrefab;
    

    public float AmountIncreaseDuration = 2;
    private float _elapsedTime = 0;

    public Image ErrorCookiesMissing;

    public float DisplayTime = 2;

    public Color Purchaseable = Color.green;
    public Color NotPurchaseable = Color.red;

    Animator AmountIncreaseAnimator;

    public void UpdateCost(double newCost)
    {
        Cost.text = newCost.ToString();
    }

    public void UpdateAmount(double newAmount)
    {
        if ((Amount.IsActive() == false))
        {
            Amount.gameObject.SetActive(true);

        }
        Amount.text = newAmount.ToString();
    }

    public void BounceText()
    {
        //create "+1" X above position of text field, float down, call "UpdateAmount" when hit

    }

    public void ShowErrorCookiesMissing(float missing)
    {
        if (IsInvoking("Hide_ErrorCookiesMissing")) {
            StopAllCoroutines();
                }
        
        ErrorCookiesMissingText.text = "Youre missing " + missing.ToString() + " cookies!";
        ErrorCookiesMissing.gameObject.SetActive(true);
        Invoke("Hide_ErrorCookiesMissing", DisplayTime); //How to save that it is already going to  fade out and set a new time? Animator easier?
    }

    private void Hide_ErrorCookiesMissing()
    {
        ErrorCookiesMissing.gameObject.SetActive(false);
    }

    public void SetCostColour(ButtonState state)
    {

        switch (state)
        {
            case ButtonState.Purchaseable:
                Cost.color = Purchaseable;
                break;

            case ButtonState.NotPurchaseable:
                Cost.color = NotPurchaseable;
                break;
            default: Cost.color = Color.white;
                Debug.LogError("No case defined for button state: " + state);
                break;
        }

    }

    private void Start()
    {
        AmountIncreaseAnimator = AmountIncreaseText.GetComponentInChildren<Animator>();
        Debug.Log(AmountIncreaseAnimator);

    }


    public void AmountIncrease(int amount) //Add: Instantiate, clone Text_AountIncrease - create AmountIncrease controller and add this to the start of it
    { //Inside Unit circle
        Vector2 OffsetPos = (Vector2)Amount.transform.position + Random.insideUnitCircle * 10;
        //How to find the AMount width , because its a rect transform (has width), but we handel it as if its a transform (only position, doesnt have width)
        
        Transform Clone = Instantiate(AmountIncreaseTextPrefab, OffsetPos, Quaternion.identity, Amount.transform);

        Text CloneText = Clone.GetComponentInParent<Text>();
        CloneText.text = ("+" + amount.ToString());


        //ISSUE On first purchase, CPC button triggers animation twice - deleting AmountIncrease fixes this. Doesnt happen for other two buttons


        /*
      AmountIncreaseAnimator.ResetTrigger("AmountIncreaseTrigger");
      AmountIncreaseAnimator.SetTrigger("AmountIncreaseTrigger");

    _elapsedTime = 0;

      while (_elapsedTime < AmountIncreaseDuration)
      {
          _elapsedTime = _elapsedTime + Time.deltaTime;
        //  Debug.Log("Position" + AmountIncreaseText.transform.position.ToString());

        //  AmountIncreaseText.rectTransform.position += Vector3.up;
        //  Debug.Log("Position" + AmountIncreaseText.transform.position.ToString() );
      }
       */

    }


}
