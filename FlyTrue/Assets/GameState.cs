using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{


    public enum State
    {
        GameReally,
        GameStart,
        GameEnd,
        GameBoss,
        GameLock,
        GameWait,
    }
    public State _State;
    // Start is called before the first frame update
    void Start()
    {
        _State = State.GameReally;
    }

    // Update is called once per frame
    void Update()
    {
        Switch();
    }



    void Switch()
    {
        switch (_State)
        {
            case State.GameReally:
                GameReally();
                break;
            case State.GameStart:
                GameStart();
                break;
            case State.GameBoss:
                GameBoss();
                break;
            case State.GameEnd:
                GameEnd();
                break;
            case State.GameLock:
                GameLock();
                break;
            case State.GameWait:
                GameWait();
                break;




        }
    }
    public void GameReally()
    {
        _State = State.GameReally;
    }
    public void GameStart()
    {
        _State = State.GameStart;
    }
    public void GameBoss()
    {
        _State = State.GameBoss;
    }
    public void GameEnd()
    {
        _State = State.GameEnd;
    }
    public void GameLock()
    {
        _State = State.GameLock;
    }

    public void GameWait()
    {
        _State = State.GameWait;
    }
}
