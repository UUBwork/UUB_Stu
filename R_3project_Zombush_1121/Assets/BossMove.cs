using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameObject MarkingPoint;
    public GameObject[] gos; //获取每个目标点，，注意数组顺序不能乱
    public float speed = 1;  //用于控制移动速度
    public bool Xspeed = false;
    public int i = 18;            //用于记录是第几个目标点
    float des;             //用于存储与目标点的距离     
                           // Use this for initialization
    public GameObject targe;
    public Rigidbody targeRigidbody;

    void Start()
    {
       
    }

    private void Awake()
    {
        targeRigidbody = targe.GetComponent<Rigidbody>();
        for (int ii = 0; ii < 75; ii++)
            gos[ii] = MarkingPoint.transform.GetChild(ii).gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        TargeDistance();
        //看向目标点
        this.transform.LookAt(targe.transform);
        //计算与目标点间的距离
        des = Vector3.Distance(this.transform.position, gos[i].transform.position);
        //移向目标
        transform.position = Vector3.MoveTowards(this.transform.position, gos[i].transform.position, Time.deltaTime * speed);
        //如果移动到当前目标点，就移动向下个目标
        if (des < 0.1f && i < 75)
        {
            i++;
        }
         if (i == 75) 
            {
                i = 0;
            } 
    }
    
    void TargeDistance()
    {

        

        if (!Xspeed)
        {
            Vector3 displacement = targeRigidbody.position - transform.position;
            float distance = displacement.magnitude;
            speed = targeRigidbody.velocity.magnitude;
            if (Vector3.Distance(targe.transform.position, transform.position) < 40f)
            {
               // speed = speed+100;
                speed= Mathf.Lerp(speed, speed + 150, 1);
            }
            else
            {
                if(targeRigidbody.velocity.magnitude<10)
               // speed = targeRigidbody.velocity.magnitude*0.1f;
                speed = Mathf.Lerp(speed, targeRigidbody.velocity.magnitude*0.1f, 0.5f);
                else
                   // speed= targeRigidbody.velocity.magnitude;
                speed = Mathf.Lerp(speed, targeRigidbody.velocity.magnitude+10, 0.5f);
            }
          
        }
        else
        {
            speed = 0;
        }
    }

}
