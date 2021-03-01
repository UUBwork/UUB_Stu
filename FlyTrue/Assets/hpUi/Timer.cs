using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int m;
    public int s;
    public int timer;
    public Text _text;
    string mm;
    string ss;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        timer = Mathf.FloorToInt(Time.time);
        m = timer / 60;
        s = timer % 60;
        if (m < 10)
        {
            mm = "0" + m;
        }
        else
        {
            mm = m.ToString();
        }
        if (s < 10)
        {
            ss = "0" + s;
        }
        else
        {
            ss = s.ToString();
        }
        _text.text = mm + ":" + ss;
    }
}
