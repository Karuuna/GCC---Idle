using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonModel : MonoBehaviour {
    public int Amount;
    public float Cost;
    public float CostScaleFactor;

    public float GetScaledCostForLevel() //Hey, Greg here. Please remind me to tell you about Properties.
        //Just set the scale factor for every button on start   mathf. for advanced math fun
    {
        return Amount * CostScaleFactor + 10;
    }






}
