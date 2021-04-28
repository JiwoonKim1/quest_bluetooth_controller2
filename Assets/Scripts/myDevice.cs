using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine.UI;

//입력이 수신되고 저장되는 형태를 나타내는 C# 구조를 생성
public struct myDeviceState : IInputStateTypeInfo
{
    // FourCC type codes are used to identify the memory layouts of state blocks. 
    public FourCC format => new FourCC('H', 'I', 'D');

    [InputControl(name = "button", layout = "Button", bit = 4)]
    public int button;

}

[InputControlLayout(stateType = typeof(myDeviceState))]
#if UNITY_EDITOR
[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
public class myDevice : InputDevice, IInputUpdateCallbackReceiver
{
    public ButtonControl button { get; private set; }

    private GameObject bluetoothControl = GameObject.FindWithTag("BlueTooth");

    //생성자
    static myDevice()
    {
        InputSystem.RegisterLayout<myDevice>(
            matches : new InputDeviceMatcher()
            .WithInterface("HID")
            .WithCapability("productId", 0)
            .WithCapability("vendorId", 65535));
    }

    protected override void FinishSetup()
    {
        base.FinishSetup();
        button = GetChildControl<ButtonControl>("button");
    }

    public void OnUpdate()
    {
        var state = new myDeviceState();
        

        InputSystem.onEvent += (eventPtr, device) =>
        {

            if (eventPtr.sizeInBytes == 28)
            {
                //bluetoothControl.GetComponent<BTtest>().changeText();
                Debug.Log(28);
                //Debug.Log(InputDevice.InvalidDeviceId);
                //Debug.Log(eventPtr.ge)
            }
            
        };
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init() { }
}

