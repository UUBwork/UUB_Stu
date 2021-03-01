using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityValue : MonoBehaviour
{
    private float _hp;
    private float _maxhp;
    private float _atk;
    private float _vit;
    private float _mp;
    private float _maxmp;
    private float _stun;
    // Use this for initialization



    public float HP
    {
        set
        {
            _hp = value;
        }
        get
        {
            return _hp;
        }
    }
    public float MaxHP
    {
        set
        {
            _maxhp = value;
        }
        get
        {
            return _maxhp;
        }
    }
    public float ATK
    {
        set
        {
            _atk = value;
        }
        get
        {
            return _atk;
        }
    }

    public float VIT
    {
        set
        {
            _vit = value;
        }
        get
        {
            return _vit;
        }
    }

    public float MP
    {
        set
        {
            _mp = value;
        }
        get
        {
            return _mp;
        }

    }
    public float MaxMP
    {
        set
        {
            _maxmp = value;
        }
        get
        {
            return _maxmp;
        }
    }
    public float Stun
    {
        set
        {
            _stun = value;

        }
        get
        {
            return _stun;
        }
    }


    public void CharacterSet(float maxhp, float hp, float maxmp, float mp, float atk, float vit)
    {
        _hp = hp;
        _maxhp = maxhp;
        _maxmp = maxmp;
        _atk = atk;
        _vit = vit;
        _mp = mp;



    }
}
