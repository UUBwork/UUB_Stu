using System.Collections.Generic;
using UnityEngine;
using Valve.VR;



//SteamVR_Fade.Start(Color.black, 1, true); 關燈
//SteamVR_Fade.Start(Color.clear, 1, true);
public class VRInput : MonoBehaviour
{
    public SteamVR_Action_Boolean TriggerClick;
    public SteamVR_Action_Boolean TouchpadTriggerClick;
    public SteamVR_Action_Vibration steamVR_Action_Vibration;
    public SteamVR_Action_Vector2 AxisVector2;

    

    static Dictionary<Input, InputButton> inputButtonDict = new Dictionary<Input, InputButton>();
    static Dictionary<Input, InputAxis> inputAxisDict = new Dictionary<Input, InputAxis>();
    static Dictionary<Input, InputTouchpadButton> InputTouchpadButtonDict = new Dictionary<Input, InputTouchpadButton>();
    static Dictionary<Onput, VR_OnputShock> VR_OnputShockDict = new Dictionary<Onput, VR_OnputShock>();
    


    private void Start() {
        
    } //Monobehaviours without a Start function cannot be disabled in Editor, just FYI
    
    public enum Input {

        LeftTrigger,
        RightTrigger,
        LeftAxis,
        RightAxis,
        LeftTouchpad,
        RightTouchpad,
    }
    public enum Onput
    {
        LeftVRShock,
        RightVRShock,
    }

    private void LateUpdate()
    {
        foreach (KeyValuePair<Input, InputButton> temp in inputButtonDict) {
            temp.Value.lateUpdateReset();
        }
        foreach (KeyValuePair<Input, InputTouchpadButton> temp in InputTouchpadButtonDict)
        {
            temp.Value.lateUpdateReset();
        }

    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        inputButtonDict.Add(Input.LeftTrigger, new InputButton(TriggerClick, SteamVR_Input_Sources.LeftHand));
        inputButtonDict.Add(Input.RightTrigger, new InputButton(TriggerClick, SteamVR_Input_Sources.RightHand));

        inputAxisDict.Add(Input.LeftAxis, new InputAxis(AxisVector2, SteamVR_Input_Sources.LeftHand));
        inputAxisDict.Add(Input.RightAxis, new InputAxis(AxisVector2, SteamVR_Input_Sources.RightHand));

        InputTouchpadButtonDict.Add(Input.LeftTouchpad, new InputTouchpadButton(TouchpadTriggerClick, SteamVR_Input_Sources.LeftHand));
        InputTouchpadButtonDict.Add(Input.RightTouchpad, new InputTouchpadButton(TouchpadTriggerClick, SteamVR_Input_Sources.RightHand));

        VR_OnputShockDict.Add(Onput.LeftVRShock, new VR_OnputShock(steamVR_Action_Vibration, SteamVR_Input_Sources.LeftHand));
        VR_OnputShockDict.Add(Onput.RightVRShock, new VR_OnputShock(steamVR_Action_Vibration, SteamVR_Input_Sources.RightHand));

    }

    



    public static bool GetButtonDown(Input input) {
        
        return inputButtonDict[input].GetButtonDown();

    }
    public static bool GetButtonUp(Input input)
    {

        return inputButtonDict[input].GetButtonUp();

    }
    public static bool GetButtonUpdate(Input input)
    {

        return inputButtonDict[input].GetButtonUpdate();

    }

    public static bool GetTouchpadButtonDown(Input input)
    {

        return InputTouchpadButtonDict[input].GetTouchpadButtonDown();

    }
    public static bool GetTouchpadButtonUp(Input input)
    {

        return InputTouchpadButtonDict[input].GetTouchpadButtonUp();

    }
    public static bool GetTouchpadButtonUpdate(Input input)
    {

        return InputTouchpadButtonDict[input].GetTouchpadButtonUpdate();

    }



    public static SteamVR_Input_Sources getFromSource(Input input)
    {

        return inputButtonDict[input].getFromSource();

    }

    public static Vector2 GetAxis(Input input)
    {
        return inputAxisDict[input].GetAxis();
    }

    public static void VRpulse(Onput onput,float duration, float frequency, float amplitude)
    {
        VR_OnputShockDict[onput].VRpulse(duration, frequency, amplitude);
    }

    //  TriggerClick.AddOnStateDownListener(Press, SteamVR_Input_Sources.LeftHand);
    public class InputButton {

        SteamVR_Action_Boolean steamVR_Action_Boolean;
        

        
        public InputButton(SteamVR_Action_Boolean steamVR_Action_Boolean, SteamVR_Input_Sources steamVR_Input_Sources) {

            this.steamVR_Action_Boolean = steamVR_Action_Boolean;
            
            // this.steamVR_Action_Boolean.AddOnStateDownListener(ButtonDown, steamVR_Action_Boolean.activeDevice);
            //his.steamVR_Action_Boolean.AddOnStateUpListener(ButtonUp, steamVR_Action_Boolean.activeDevice);

            this.steamVR_Action_Boolean.AddOnStateDownListener(ButtonDown, steamVR_Input_Sources);

            this.steamVR_Action_Boolean.AddOnStateUpListener(ButtonUp, steamVR_Input_Sources);
            
         
        }

        bool isDown;
        bool isUp;
        bool isUpdate;
       public  void lateUpdateReset() {
            isDown = false;
            isUp = false;
        }

        void ButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {

            
            isDown = true;
            isUpdate = true;
        }
        void ButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) 
        {
            isUp = true;
            isUpdate = false;
        }

        public bool GetButtonDown() {
            return isDown;
        }
        public bool GetButtonUpdate()
        {
            return isUpdate;
        }
        public bool GetButtonUp() {
            return isUp;
        }
        public SteamVR_Input_Sources getFromSource()
        {
            return steamVR_Action_Boolean.activeDevice;
        }


        

    }

    public class InputAxis
    {
        public SteamVR_Action_Vector2 steamVR_Action_Vector2;
        
        public InputAxis(SteamVR_Action_Vector2 steamVR_Action_Vector2, SteamVR_Input_Sources steamVR_Input_Sources)
        {
            this.steamVR_Action_Vector2 = steamVR_Action_Vector2;
            this.steamVR_Action_Vector2.AddOnAxisListener(Axis, steamVR_Input_Sources);
            
        }
        Vector2 _axis;

        void Axis(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource,Vector2 a,Vector2 b)
        {
           
            //Debug.Log(this.steamVR_Action_Vector2.GetAxis(fromSource));
            _axis=this.steamVR_Action_Vector2.GetAxis(fromSource);
            //return null;
        }
        public Vector2 GetAxis()
        {
           
            return _axis;
        }
      

    }

    public class InputTouchpadButton
    {
        SteamVR_Action_Boolean steamVR_Action_Boolean;



        public InputTouchpadButton(SteamVR_Action_Boolean steamVR_Action_Boolean, SteamVR_Input_Sources steamVR_Input_Sources)
        {

            this.steamVR_Action_Boolean = steamVR_Action_Boolean;

            // this.steamVR_Action_Boolean.AddOnStateDownListener(ButtonDown, steamVR_Action_Boolean.activeDevice);
            //his.steamVR_Action_Boolean.AddOnStateUpListener(ButtonUp, steamVR_Action_Boolean.activeDevice);

            this.steamVR_Action_Boolean.AddOnStateDownListener(ButtonDown, steamVR_Input_Sources);

            this.steamVR_Action_Boolean.AddOnStateUpListener(ButtonUp, steamVR_Input_Sources);


        }

        bool isDown;
        bool isUp;
        bool isUpdate;
        public void lateUpdateReset()
        {
            isDown = false;
            isUp = false;
        }

        void ButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {


            isDown = true;
            isUpdate = true;
        }
        void ButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            isUp = true;
            isUpdate = false;
        }

        public bool GetTouchpadButtonDown()
        {
            return isDown;
        }
        public bool GetTouchpadButtonUpdate()
        {
            return isUpdate;
        }
        public bool GetTouchpadButtonUp()
        {
            return isUp;
        }
        public SteamVR_Input_Sources getTouchpadFromSource()
        {
            return steamVR_Action_Boolean.activeDevice;
        }
    }

    public class VR_OnputShock
    {
        SteamVR_Action_Vibration steamVR_Action_Vibration;
        SteamVR_Input_Sources steamVR_Input_Sources;
        public VR_OnputShock(SteamVR_Action_Vibration steamVR_Action_Vibration, SteamVR_Input_Sources steamVR_Input_Sources)
        {
            this.steamVR_Action_Vibration = steamVR_Action_Vibration;
            this.steamVR_Input_Sources = steamVR_Input_Sources;
        }



        public void VRpulse(float duration, float frequency, float amplitude)
        {
            VRInternalpulse(duration, frequency, amplitude, steamVR_Input_Sources);
        }

        void VRInternalpulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources steamVR_Input_Sources)
        {
            steamVR_Action_Vibration.Execute(0, duration, frequency, amplitude, steamVR_Input_Sources);
        }


    }

}