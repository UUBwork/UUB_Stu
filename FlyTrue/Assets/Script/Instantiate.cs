using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{

    public float delay=5.0f; 
    public GameObject enemyA;
    public GameObject enemyB;

    public GameObject enemyC;
    public GameObject enemyD;
    public GameObject enemyE0;
    public GameObject enemyE1;
    public GameObject enemyE2;
    public float StartTime=0f;
    float timer;
    public EmenyState _EmenyState;
    public enum EmenyState
    {
      
        BAKE,
        FighterMedium_FBX,
        SpaceShuttle_01,
        BoomMonster,
        Rock,
    }
    public bool isStart=false;

    
    void Update()
    {

        if (isStart)
            switch (_EmenyState)
            {
                case EmenyState.BAKE:
                    BAKE();
                    break;
                case EmenyState.FighterMedium_FBX:
                    FighterMedium_FBX();
                    break;
                case EmenyState.SpaceShuttle_01:
                    SpaceShuttle_01();
                    break;

                case EmenyState.BoomMonster:
                    BoomMonster();
                    break;
                case EmenyState.Rock:
                    Rock();
                    break;
            }
        StartTime = Mathf.FloorToInt(Time.time);
        if (StartTime > 120)
        {
            delay = 3f;
        }

    }



    void Rock()
    {
        timer += Time.deltaTime;
        if (timer >= delay && StartTime >= 10)
        {
            timer = 0;
            if (Random.Range(0, 10.0f) >= 8.0f)
            {
                print("爆炸怪");
                if(Random.Range(0, 3.0f) >= 2.0f)
                Instantiate(enemyE1, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
                else if (Random.Range(0, 3.0f) >= 1.0f)
                    Instantiate(enemyE2, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
                else if (Random.Range(0, 3.0f) >= 0.0f)
                    Instantiate(enemyE0, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
            }
        }
    }

    void BoomMonster() {

        timer += Time.deltaTime;
        if (timer >= delay && StartTime >= 30)
        {
            timer = 0;
            if (Random.Range(0, 10.0f) >= 6.0f)
            {
                print("爆炸怪");
                Instantiate(enemyD, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
            }
        }
    }

    void BAKE()
    {
        
        timer += Time.deltaTime;
        
        if (timer >= delay&& StartTime>=45)
        {
            timer = 0;
            if (Random.Range(0, 10.0f) >= 6.0f)
            {
                Instantiate(enemyA, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
            }
        }
    }
    void FighterMedium_FBX()
    {
        
        timer += Time.deltaTime;

        if (timer >= delay && StartTime >= 15)
        {
            timer = 0;
            if (Random.Range(0, 10.0f) >= 6.0f)
            {
                Instantiate(enemyB, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
            }
        }
    }
    void SpaceShuttle_01()
    {
      
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            timer = 0;
            if (Random.Range(0, 10.0f) >= 6.0f)
            {
                Instantiate(enemyC, this.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), Random.Range(-10f, 10f)), this.transform.rotation);
            }
        }
    }


}


      
