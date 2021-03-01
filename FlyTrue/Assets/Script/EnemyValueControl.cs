using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValue 
{
    int _MaxHP;
    int _HP;
    int _Atk;
    

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

    public void Set(int maxhp,int hp, int atk)
    {
        MaxHP = maxhp;
        HP = hp;
        Atk = atk;
    }


}


public class EnemyValueControl : MonoBehaviour
{

    EnemyValue _EnemyValue = new EnemyValue();

    public void Hit(int atk)
    {
        _EnemyValue.HP -= atk;
    }

    public int NowHP()
    {
        return _EnemyValue.HP;
    }

    public int Atk()
    {
        return _EnemyValue.Atk;
    }

    public void SetValue(int maxhp, int hp, int atk)
    {
        _EnemyValue.Set(maxhp, hp, atk);
    }

    public void Recovery(int a)
    {

    }





}
