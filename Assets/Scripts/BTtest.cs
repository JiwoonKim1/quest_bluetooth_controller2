using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class BTtest : MonoBehaviour
{
    [SerializeField] private Text pressedText;
    [SerializeField] private Text connectedText;

    //private myDeviceState state;
    private int pushed = 0;
    private int connected = 0;

    private void Start()
    {
        pressedText.text = pushed.ToString();

        connected = InputSystem.devices.Count;
        connectedText.text = connected.ToString();
        //state = new myDeviceState();

        // add our listener to the onDeviceChange event
        InputSystem.onDeviceChange += onInputDeviceChange;
        InputSystem.onEvent += onEvent;
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

        connectedText.text = connected.ToString();
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        //Debug.Log("onEvent");

        if (inputEvent.sizeInBytes == 28)
        {
            changeText();
            Debug.Log(device.deviceId);
        }
    }

    public void changeText()
    {
        pushed += 1;
        pressedText.text = pushed.ToString();
    }
}
