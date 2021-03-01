using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class TestTeleporrt : MonoBehaviour
{
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;
    private SteamVR_Behaviour_Pose m_pose = null;
    //是否有有效的位置
    private bool m_HasPostion = false;
    //是否有在传送
    private bool m_IsTeleporting = false;
    [Header("延迟时间")]
    [Range(0.2f, 1)] public float m_fadeTime = 0.5f;
    public SteamVR_Camera playerCamera;

    private void Awake()
    {
        m_pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        //这个parabola是我自己写的抛物线检测脚本
        //Pointer（bool）检测到地板时返回的时true 
      //  m_HasPostion = parabola.Pointer;
        if (m_TeleportAction.GetLastStateDown(m_pose.inputSource))
        {
            TryTelePort();
        }
    }

    private void TryTelePort()
    {
        //  检查有效位置，如果已经传送
        if (!m_HasPostion || m_IsTeleporting)
        {
            return;
        }

        //获取相机和头的位置
        Transform cameraRig = playerCamera.origin;
        Vector3 HeadPostion = playerCamera.head.position;

        //地面的位置
        Vector3 groundPostion = new Vector3(HeadPostion.x, cameraRig.position.y, HeadPostion.z);
        //目标点的位置-开始位置=移动的距离
        Vector3 translateVector = m_Pointer.transform.position - groundPostion;

        //移动
        //StartCoroutine(MoveRig(cameraRig, translateVector));
    }

    /*private IEnumerator MoveRig(Transform cameraRig, Vector3 traslation)
    {
        m_IsTeleporting = true;

        //画面变黑
        SteamVR_Fade.Start(Color.black, m_fadeTime, true);

        //延迟显示画面
        yield return new WaitForSeconds(m_fadeTime);
        //player移动位置
        cameraRig.position += traslation;

        //画面逐渐变亮
        SteamVR_Fade.Start(Color.clear, m_fadeTime, true);

        //De-flag
        m_IsTeleporting = false;

        yield return null;
    }*/
}