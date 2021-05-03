using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class BTtest : MonoBehaviour
{
    [SerializeField] private Text pressedText;
    [SerializeField] private Text connectedText;

    //private myDeviceState state;
    private int pushed = 0;
    private int connected = 0;
    private bool flag = false;


    private void Start()
    {
        pressedText.text = pushed.ToString();

        connected = InputSystem.devices.Count;
        connectedText.text = connected.ToString();
        //state = new myDeviceState();

        // add our listener to the onDeviceChange event
        InputSystem.onDeviceChange += onInputDeviceChange;

        InputSystem.onEvent += onEvent;
        InputSystem.onEvent += ListenToEvents;
    }

    void Update()
    {

        if (Input.anyKeyDown)
        {
            pushed += 1;
            pressedText.text = pushed.ToString();
        }
        

    }

    public void onInputDeviceChange(InputDevice device, InputDeviceChange change)
    {
        //Debug.Log("onInputDeviceChange");
        switch (change)
        {
            case InputDeviceChange.Added:
                connected += 1;
                //Debug.Log("Device added: " + device);
                break;
            case InputDeviceChange.Removed:
                connected -= 1;
                //Debug.Log("Device removed: " + device);
                break;
            case InputDeviceChange.ConfigurationChanged:
                //Debug.Log("Device configuration changed: " + device);
                break;
            case InputDeviceChange.Disconnected:
                connected -= 1;
                //Debug.Log("Device Disconnected: " + device);
                break;
            case InputDeviceChange.Reconnected:
                connected += 1;
                //Debug.Log("Device Reconnected: " + device);
                break;
        }

        Debug.Log(device.usages);

        if(connected != null) connectedText.text = connected.ToString();
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        //Debug.Log(inputEvent.GetType());
        //Debug.Log(inputEvent.valid);
        if (inputEvent.sizeInBytes == 28)
        {
            if (flag)
            {
                changePressedText();
                flag = false;
            }
            else
            {
                flag = true;
            }
            Debug.Log(device.deviceId);
        }
    }

    public void changePressedText()
    {
        pushed += 1;
        //GetComponent<writeCSV>().writeTime(t)
        if(pressedText != null) pressedText.text = pushed.ToString();
    }

    public void changeConnectedText()
    {
        if(connectedText != null) connectedText.text = connected.ToString();
    }

    public void ListenToEvents(InputEventPtr eventPtr, InputDevice device_in)
    {
        // Ignore anything that isn't a state event.
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        var keyboard = device_in as Keyboard;   //Test for keyboard event
        var mydevice = device_in as myDevice;

        // Let's say we get some arbitrary InputDevice here as "device".
        // We allocate some temp memory for a StateEvent and copy
        // the device's current state into it (both is done by From()).

        using (StateEvent.From(device_in, out var OutEventPtr))
        {
            // Let's say we want to toggle on all buttons on the device.
            foreach (var control in device_in.allControls)
            {
                if (control is ButtonControl button)
                {
                    //int a = 1;
                    //button.WriteValueIntoEvent<int>(1, OutEventPtr);
                }
            }
            InputSystem.QueueEvent(OutEventPtr);
        }
        

        if (mydevice != null)
        {
            Debug.Log("mydevice");

            //object state = device_in.ReadValueFromStateAsObject(device_in.GetStatePtrFromStateEvent(eventPtr));
        }
        if (keyboard != null)
        {
            //Need to take the eventPtr and get the Key press data  **HELP!!!**
            return;
        }

        var mouse = device_in as Mouse;   //Test for mouse event
        if (mouse != null)
        {
            //Need to take the eventPtr and get the Mouse button press data **HELP!!!**
        }

    } // End Listener
}
