using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour {
    public Text HP_Text;
    public c_AbilityValue c_AbilityValue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HP_Text.text = c_AbilityValue.HP.ToString();

    }
}
