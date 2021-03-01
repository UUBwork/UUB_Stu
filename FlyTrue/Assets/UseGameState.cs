using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGameState : MonoBehaviour
{
    public GameState _gameState;
    public ObjectState _ObjectState;
    public enum ObjectState
    {
        Player,
        Enemy,
        Boss,
        Instantiate,
        FollowMove,
        UI,

    }

    private void Start()
    {
        _gameState= GameObject.Find("GameState").GetComponent<GameState>();
    }

    void Update()
    {


      

        Switch();

        



    }


    void Switch()
    {
        switch (_ObjectState)
        {
            case ObjectState.Player:
                Player();
                break;
            case ObjectState.Enemy:
                Enemy();
                break;
            case ObjectState.Boss:
                Boss();
                break;
            case ObjectState.Instantiate:
                Instantiate();
                break;
            case ObjectState.FollowMove:
                FollowMove();
                break;
            case ObjectState.UI:
                UI();
                break;

        }
    }

    public Player _Player;
    void Player()
    {
        if (_Player == null)
        {
            _Player = GetComponent<Player>();
        }


        if (_gameState._State == GameState.State.GameReally)
        {
            _Player._playerMove = global::Player.PlayerMove.LineMove;
        }
        else if (_gameState._State == GameState.State.GameStart)
        {
            _Player._playerMove = global::Player.PlayerMove.LineMove;
        }
        else if (_gameState._State == GameState.State.GameBoss)
        {
            _Player._playerMove = global::Player.PlayerMove.BossToMove;
        }
        else if (_gameState._State == GameState.State.GameEnd)
        {

        }else if(_gameState._State == GameState.State.GameLock)
        {

        }
        else if (_gameState._State == GameState.State.GameWait)
        {
            _Player._playerMove = global::Player.PlayerMove.LineMove;
        }
    }

    void Enemy()
    {
        if (_gameState._State == GameState.State.GameReally)
        {
            this.gameObject.SetActive(false);
        }
        else if (_gameState._State == GameState.State.GameStart)
        {
            this.gameObject.SetActive(true);
        }
        else if (_gameState._State == GameState.State.GameBoss)
        {
            Destroy(this.gameObject);
        }
        else if (_gameState._State == GameState.State.GameEnd)
        {
            Destroy(this.gameObject);
        }
        else if (_gameState._State == GameState.State.GameLock)
        {

        }
        else if (_gameState._State == GameState.State.GameWait)
        {
            Destroy(this.gameObject);
        }
    }

    public BossEnemy _BossEnemy;
    public BoxCollider _BoxCollider;
    void Boss()
    {
        if (_BossEnemy == null)
        {
            _BossEnemy = GetComponent<BossEnemy>();
            _BoxCollider= GetComponent<BoxCollider>();
        }
        if (_gameState._State == GameState.State.GameReally)
        {
            _BossEnemy.StartATK = false;
            
        }
        else if (_gameState._State == GameState.State.GameStart)
        {

        }
        else if (_gameState._State == GameState.State.GameBoss)
        {
            _BossEnemy.StartATK = true;
        }
        else if (_gameState._State == GameState.State.GameEnd)
        {

        }
        else if (_gameState._State == GameState.State.GameLock)
        {

        }
        else if (_gameState._State == GameState.State.GameWait)
        {

        }
    }
    public Instantiate _Instantiate;
    void Instantiate()
    {
        if (_Instantiate == null)
        {
            _Instantiate = GetComponent<Instantiate>();
        }
        if (_gameState._State == GameState.State.GameReally)
        {
            _Instantiate.isStart = false;
        }
        else if (_gameState._State == GameState.State.GameStart)
        {

            _Instantiate.isStart = true;
        }
        else if (_gameState._State == GameState.State.GameBoss)
        {
            Destroy(this.gameObject);
        }
        else if (_gameState._State == GameState.State.GameEnd)
        {
            Destroy(this.gameObject);
        }
        else if (_gameState._State == GameState.State.GameLock)
        {

        }
        else if (_gameState._State == GameState.State.GameWait)
        {
            Destroy(this.gameObject);
        }
    }

        public FollowMove _FollowMove;

    void FollowMove()
        {
            if (_FollowMove == null)
            {
            _FollowMove = GetComponent<FollowMove>();
            }
            if (_gameState._State == GameState.State.GameReally)
            {
            _FollowMove.OpevValue = false;
            }
            else if (_gameState._State == GameState.State.GameStart)
            {

                
            }
            else if (_gameState._State == GameState.State.GameBoss)
            {
            _FollowMove.OpevValue = true;
            }
            else if (_gameState._State == GameState.State.GameEnd)
            {
               
            }
            else if (_gameState._State == GameState.State.GameLock)
            {

            }
            else if (_gameState._State == GameState.State.GameWait)
            {
               
            }

        }

    btn_Start _btn_Start;
    public GameObject uiReStart;
    BoxCollider _uiBoxCollider;
     void UI()
    {
        if (this.gameObject.name == "RESTART")
        {
            if (_uiBoxCollider == null)
            {
                _uiBoxCollider = GetComponent<BoxCollider>();
                

            }

            if (_gameState._State == GameState.State.GameReally)
            {
                uiReStart.SetActive(false);
                _uiBoxCollider.enabled = false;
            }

            if (_gameState._State == GameState.State.GameEnd)
            {
                uiReStart.SetActive(true);
                _uiBoxCollider.enabled = true;
            }
        }
        else
        {
            if (_btn_Start == null)
            {
                _btn_Start = GetComponent<btn_Start>();
            }
            if (_gameState._State == GameState.State.GameReally)
            {
                _btn_Start.enabled = false;
            }
            else if (_gameState._State == GameState.State.GameWait)
            {
                _btn_Start.enabled = true;
                _btn_Start.open();
            }

        }

    }


}
