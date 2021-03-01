using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_UI : MonoBehaviour
{

    public Text _text_Hp;


    // Start is called before the first frame update
    void Start()
    {
        _text_Hp=GameObject.Find("HP").GetComponent<Text>();
    }

    
    void Update()
    {
        SetHP();
    }


    void SetHP()
    {
        _text_Hp.text = GameObject.Find("Player").GetComponent<Player>().GetHP().ToString();
    }
}
