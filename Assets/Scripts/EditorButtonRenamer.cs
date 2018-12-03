using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class EditorButtonRenamer : MonoBehaviour {


    public UpgradeButtonView View_CPS1;
    public UpgradeButtonView View_Autoclicker;
    public UpgradeButtonView View_UpgradeCookiesPerClick;

    // Use this for initialization
    void Start () {
        View_CPS1.Label.text = View_CPS1.gameObject.name;
        View_Autoclicker.Label.text = View_Autoclicker.gameObject.name;
        View_UpgradeCookiesPerClick.Label.text = View_UpgradeCookiesPerClick.gameObject.name;
    }

	// Update is called once per change in the scene if "Execute inEditMode" is set
	void Update () {
		
	}
}
