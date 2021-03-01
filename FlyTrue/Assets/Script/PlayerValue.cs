using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValue 
{
    int _HP;
    int _Atk;
    int _MaxHP;


    public int MaxHP
    {
        get { return _MaxHP; }
        set { _MaxHP = value; }
    }

    public int HP
    {
        get { return _HP; }
        set { _HP = value; }
    }

    public int Atk
    {
        get { return _Atk; }
        set { _Atk = value; }
    }

    public void Set(int hp, int atk)
    {
        MaxHP = hp;
        HP = hp;
        Atk = atk;
    }
}
