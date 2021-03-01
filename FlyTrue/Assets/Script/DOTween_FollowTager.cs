using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTween_FollowTager : MonoBehaviour
{
    public int num;
    public GameObject GameObjectGrounp;
    public GameObject[] TargeGameObject;
     Vector3[] TargeV3;
    Tweener tween;
    public float FinishTime;

    public Player _player;
    public GameState _GameState;

    public GameObject BossGO;
    public Vector3 BossV3;
    private void Awake()
    {
        
        TargeGameObject = new GameObject[num];
        TargeV3 = new Vector3[num];
        for (int i=0;i< num; i++)
        {
            TargeGameObject[i] = GameObjectGrounp.transform.GetChild(i).gameObject;
            TargeV3[i] = TargeGameObject[i].transform.position;
        }
     //   BossV3 = BossGO.transform.position;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        // print("132"+tween.IsActive()); //都可 但完成回傳F
        if(isStart)
        if (!tween.IsActive())
        {
                _player._playerMove = Player.PlayerMove.PlayerWait;
                _GameState._State = GameState.State.GameWait;
                isStart = false;
                print("//");
                isStartGOboss = true;
        }
        
        //print("PLAY"+tween.IsPlaying());

    }


    public bool isStart = false;
    public void StartMove()
    {
        isStart = true;

        tween = transform.DOLocalPath(TargeV3, FinishTime, PathType.Linear, PathMode.Full3D).SetLookAt(0.05F, -Vector3.right);
    }


    public bool isStartGOboss = false;
 /*   public void GoBossPos()
    {

        if (isStartGOboss)
        {

        }

    }

    */

}
