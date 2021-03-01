using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reduce : MonoBehaviour {

    public Image _image;
    public float hp;
    public float MaxHP;
    public c_AbilityValue _c_AbilityValue;
    private void Awake()
    {
        
    }


    private void Update()
    {
        hp = _c_AbilityValue.HP;
        MaxHP = _c_AbilityValue.MaxHP;
        _image.fillAmount = hp / MaxHP;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }







}
