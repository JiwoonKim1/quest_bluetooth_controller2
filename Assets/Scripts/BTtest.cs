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

        Debug.Log(InputDevice.all);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            pushed += 1;
            pressedText.text = pushed.ToString();
        }
        
        InputSystem.onEvent += (eventPtr, device) =>
        {

            if (eventPtr.sizeInBytes == 28)
            {
                changeText();
                //Debug.Log(28);
                Debug.Log(device.deviceId);
                //if (device.HasValueChangeInEvent(eventPtr)) Debug.Log(state.reportId);
                //Debug.Log(InputState.updateCount.ToString());
                //if (device.HasValueChangeInState()) Debug.Log(state.reportId);
            }

            //Debug.Log(device.deviceId);
        };

        InputSystem.onDeviceChange += (device, change) =>
        {
            
            switch (change)
            {
                case InputDeviceChange.Added:
                    connected += 1;
                    //Debug.Log("New device added: " + device);
                    break;

                case InputDeviceChange.Removed:
                    connected -= 1;
                    //Debug.Log("Device removed: " + device);
                    break;
                case InputDeviceChange.Disconnected:
                    connected -= 1;
                    Debug.Log("Device Disconnected: " + device);
                    break;
                case InputDeviceChange.Reconnected:
                    connected += 1;
                    Debug.Log("Device Reconnected: " + device);
                    break;
            }

            connectedText.text = connected.ToString();

        };

    }

    public void changeText()
    {
        pushed += 1;
        pressedText.text = pushed.ToString();
    }
}
