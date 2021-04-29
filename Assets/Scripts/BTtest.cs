using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class BTtest : MonoBehaviour
{
    [SerializeField] private Text myText;

    private int pushed = 0;
    private myDeviceState state;
    private int deviceId;

    private void Start()
    {
        myText.text = pushed.ToString();
        state = new myDeviceState();

        Debug.Log(InputDevice.all);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            pushed += 1;
            myText.text = pushed.ToString();
        }
        
        InputSystem.onEvent += (eventPtr, device) =>
        {

            if (eventPtr.sizeInBytes == 28)
            {
                changeText();
                Debug.Log(28);
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
                    Debug.Log("New device added: " + device);
                    break;

                case InputDeviceChange.Removed:
                    Debug.Log("Device removed: " + device);
                    break;
            }
        };

    }

    public void changeText()
    {
        pushed += 1;
        myText.text = pushed.ToString();
    }
}
