using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reduce : MonoBehaviour {

    public Image _image;
    public float hp;
    public float MaxHP;
    
    public Player _player;
    public float aaa;



    private void Start()
    {
        MaxHP = _player.GetHP();
    }

    private void Update()
    {
        hp = _player.GetHP();
 
        _image.fillAmount = hp / MaxHP;
        
    }

   







}
