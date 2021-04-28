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

    //생성자
    static myDevice()
    {
        InputSystem.RegisterLayout<myDevice>(
            matches : new InputDeviceMatcher()
            .WithInterface("HID")
            .WithCapability("productId", 0)
            .WithCapability("vendorId", 65535));
    }

    // This is only to trigger the static class constructor to automatically run
    // in the player.
    /*
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeInPlayer() { Debug.Log("Startup"); }
    */


    protected override void FinishSetup()
    {
        base.FinishSetup();
        button = GetChildControl<ButtonControl>("button");
    }

    public void OnUpdate()
    {
        var state = new myDeviceState();

        /*
        var trace = new InputEventTrace();
        trace.Enable();

        //Debug.Log(trace.GetType());

        var current = new InputEventPtr();
        while (trace.GetNextEvent(ref current))
        {
            Debug.Log("Got some event: " + current);
        }

        // Also supports IEnumerable.
        foreach (var eventPtr in trace)
            Debug.Log("Got some event: " + eventPtr);

        // Trace consumes unmanaged resources. Make sure to dispose.
        trace.Dispose();
        */

        InputSystem.onEvent += (eventPtr, device) =>
        {

            if (eventPtr.sizeInBytes == 28) Debug.Log(28);
            
        };
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init() { }
}

