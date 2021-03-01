using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class btn_Start : MonoBehaviour
{
    public GameObject player;
    public DOTween_FollowTager _DOTween_FollowTager;
    public GameState _GameState;
    public AudioSource _AudioSource;

    private void Start()
    {
        // GoMain();
        
        _AudioSource = this.GetComponent<AudioSource>();
       // _AudioSource.Play();
    }

    public void GoMain()
    {

        _DOTween_FollowTager.StartMove();
        _GameState.GameStart();
        _AudioSource.Play();
        StartCoroutine("DelayTime", 0.5f);




    }

    public void GoBossStart()
    {
        _AudioSource.Play();
        _GameState.GameBoss();
        StartCoroutine("DelayTime", 0.5f);
    }
    public GameObject UI;
    public void open()
    {
        UI.SetActive(true);
    }
    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
