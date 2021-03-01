using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Speed_UI : MonoBehaviour {
    public Text Speed_Text;
    public CarC CarC;
    public GameObject _Image;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Speed_Text.text = ((int)(CarC.CurrentSpeed*1.5F)).ToString();
        _Image.transform.localRotation = Quaternion.Euler(0, 0, 180.526f - (CarC.CurrentSpeed * 1.5F)* 2.4574f);
    }
}
