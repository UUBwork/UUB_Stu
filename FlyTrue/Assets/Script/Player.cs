using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerInput _playerInput;
    // public testInput _testInput;
    public float _speed;
    public GameObject body;
    PlayerValue _PlayerValue = new PlayerValue();
    public AircraftMoveR _AircraftMoveR;
    public GameObject core;
    public GameState _GameState;


    public enum PlayerMove
    {
        PlayerWait,
        BossToMove,
        LineMove,

    }

    public PlayerMove _playerMove;


    private void Awake()
    {

        _playerInput = this.GetComponent<PlayerInput>();

        body = GameObject.FindWithTag("PlayerBody");
        _PlayerValue.Set(200, 1);
    }
    public Animation _animation;
    private void Update()
    {

      
        PlayerState();



        Switch();


        }



    void Switch()
    {

        switch (_playerMove)
        {

            case PlayerMove.PlayerWait:
                PlayerWait();
                break;
            case PlayerMove.BossToMove:
                BossMove();
                break;
            case PlayerMove.LineMove:
                HorizontalQualityMove();
                break;


        }


    }


    void PlayerState()
    {
        if (_PlayerValue.HP <= 0)
        {
            Destroy(this.gameObject);

        }
    }


    
    void PlayerWait()
    {
        if(VRInput.GetButtonDown(VRInput.Input.RightTrigger)&& VRInput.GetButtonDown(VRInput.Input.LeftTrigger))
        {
            _playerMove = PlayerMove.BossToMove;
            _GameState._State = GameState.State.GameBoss;
        }
    }


    //移動需要 平滑 未完成
    void HorizontalQualityMove()
    {
  

        if (_AircraftMoveR.getMoveValue().z>0)
        {
            transform.position = transform.position - transform.forward * Time.deltaTime * 5* _AircraftMoveR.getMoveValue().z*0.5F;
        }
        if (_AircraftMoveR.getMoveValue().z < 0)
        {
            transform.position = transform.position - transform.forward * Time.deltaTime * 5* _AircraftMoveR.getMoveValue().z * 0.5F;
        }
        

    }




    public int GetAtk()
    {
        return _PlayerValue.Atk;
    }

    

    public void Hit(int afk)
    {
        _PlayerValue.HP = _PlayerValue.HP - afk;
        _animation.Play();
    }

    public int GetHP()
    {
        return _PlayerValue.HP;

    }



    public Transform Targe;

    public float x;
    public float z;

    public float xSpeed = 10;
    public float zSpeed = 10;

    public float distence;


    public float minDisyence;
    public float maxDisyence;


    public Quaternion rotationEuler;
    public Vector3 PlayerPos;


    public GameObject MT;

    
     
    void BossMove()
    {
        /*
        if (_AircraftMoveR.getMoveValue().z > 0)
        {
            x = x + xSpeed * _AircraftMoveR.getMoveValue().z * Time.deltaTime;
        }
        if (_AircraftMoveR.getMoveValue().z < 0)
        {
            x = x + xSpeed * _AircraftMoveR.getMoveValue().z * Time.deltaTime;
        }

        if (_AircraftMoveR.getMoveValue().x > 0 && z >= minDisyence + 1)
        {
            z = z - zSpeed * _AircraftMoveR.getMoveValue().x * Time.deltaTime;
        }
        if (_AircraftMoveR.getMoveValue().x < 0 && z <= maxDisyence - 1)
        {
            z = z - zSpeed * _AircraftMoveR.getMoveValue().x * Time.deltaTime;
        }

        check();
        */
        transform.rotation = MT.transform.rotation;
        transform.position = MT.transform.position;

    }
        


    void lockMove()
    {


        distence = Mathf.Clamp(z, minDisyence, maxDisyence);

        rotationEuler = Quaternion.Euler(0, x, 0);

        PlayerPos = rotationEuler * new Vector3(0, 0, -distence) + Targe.position;

        transform.rotation = rotationEuler;
        transform.position = PlayerPos;
    }


    private Transform myTransform;
    private Vector3 targetPosition;
    private float maxDistance;




    bool F = true;
    void check()
    {
        myTransform = transform;
        targetPosition = new Vector3(Targe.position.x, Targe.position.y, Targe.position.z);

        maxDistance = Vector3.Distance(targetPosition, myTransform.position);
        if (maxDistance <= 10)
        {

            if (F)
            {
                z = maxDistance;

                Vector3 relative = transform.InverseTransformPoint(Targe.transform.position);
                x = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                F = false;

            }
            lockMove();


        }
        else
        {



        }

    }


}
