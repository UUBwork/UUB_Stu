using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartController : MonoBehaviour
{
    public Text _Text;
    public GameObject RobotInstantiate;
    public Player _Player;
    public int WaitTime;
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    private void Awake()
    {
        timer = 0;
        CloseALL();
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    void CloseALL()
    {
        RobotInstantiate.SetActive(false);
        _Player.enabled=false;
    }
    void OpenALL()
    {
        RobotInstantiate.SetActive(true);
        _Player.enabled = true;
    }
    void StartTimer()
    {
       
        timer = Mathf.FloorToInt(Time.time);
        _Text.text=(WaitTime - timer).ToString();
        if(WaitTime==timer)
        {
            OpenALL();
            _Text.text = "";
            Destroy(this.gameObject);
        }

    }
}
