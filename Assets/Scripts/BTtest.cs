using System;
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

    private int pushed = 0;
    private int connected = 0;
    private bool flag = true;


    private void Start()
    {
        pressedText.text = pushed.ToString();

        connected = InputSystem.devices.Count;
        connectedText.text = connected.ToString();

        InputSystem.onDeviceChange += onInputDeviceChange;
        InputSystem.onEvent += onEvent;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        GetComponent<writeCSV>().writeTimeLine();
    }

    public void onInputDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                connected += 1;
                break;
            case InputDeviceChange.Removed:
                connected -= 1;
                break;
            case InputDeviceChange.ConfigurationChanged:
                break;
            case InputDeviceChange.Disconnected:
                connected -= 1;
                break;
            case InputDeviceChange.Reconnected:
                connected += 1;
                break;
        }
        if (connected != null) changeConnectedText();
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        float time = (float)Math.Round(inputEvent.time, 3);

        var keyboard = device as Keyboard;
        var mydevice = device as myDevice;

        if(keyboard != null)
        {
            changePressedtext(time);
            Debug.Log("time: " + time + ", deviceID: " + device.deviceId);
            Debug.Log("keyboard");
        }
        if(mydevice != null)
        {
            Debug.Log("time: " + time + ", deviceID: " + device.deviceId);
            if (!flag)
            {
                flag = true;
                return;
            }
            flag = false;
            changePressedtext(time);
        }
    }

    private void changePressedtext(float time)
    {
        GetComponent<writeCSV>().writeTime(time);

        pushed += 1;
        if (pressedText != null) pressedText.text = pushed.ToString();
    }

    public void changeConnectedText()
    {
        if(connectedText != null) connectedText.text = connected.ToString();
    }
}
