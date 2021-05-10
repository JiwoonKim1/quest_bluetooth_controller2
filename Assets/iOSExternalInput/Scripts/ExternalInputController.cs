using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ExternalInputDidReceiveKey(KeyCode receivedKey);
public delegate void ExternalInputDidBeginPressForKey(KeyCode pressedKey);
public delegate void ExternalInputDidEndPressForKey(KeyCode releasedKey);
public delegate void ExternalInputDidChangePressForKey(KeyCode changedKey);
public delegate void ExternalInputDidReceiveForceValue(float keyPressForce);
public delegate void ExternalInputDidCancelPressForKey(KeyCode cancelledKey);

public class ExternalInputController : MonoBehaviour {

	public static ExternalInputController instance = null;

	public event ExternalInputDidReceiveKey OnExternalInputDidReceiveKey;
	public event ExternalInputDidBeginPressForKey OnExternalInputDidBeginPressForKey;
	public event ExternalInputDidEndPressForKey OnExternalInputDidEndPressForKey;
	public event ExternalInputDidChangePressForKey OnExternalInputDidChangePressForKey;
	public event ExternalInputDidReceiveForceValue OnExternalInputDidReceiveForceValue;
	public event ExternalInputDidCancelPressForKey OnExternalInputDidCancelPressForKey;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		} else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		ExternalInputInterface.SetupExternalInput();
	}

    public void DidReceiveKeystroke(string receivedKey)
	{
		KeyCode receivedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), receivedKey);

		if(OnExternalInputDidReceiveKey != null)
			OnExternalInputDidReceiveKey(receivedKeyCode);
	}

	public void DidBeginPressForKey(string receivedKey)
    {
		// key down
		KeyCode receivedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), receivedKey);

		print("press began for key " + receivedKey);

		if(GameObject.Find("Text - Detection Label"))
			GameObject.Find("Text - Detection Label").GetComponent<Text>().text = receivedKey + " down";

		if (OnExternalInputDidBeginPressForKey != null)
			OnExternalInputDidBeginPressForKey(receivedKeyCode);
	}

	public void DidEndPressForKey(string receivedKey)
	{
		// key up
		KeyCode receivedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), receivedKey);

		print("press ended for key " + receivedKey);

		if(GameObject.Find("Text - Detection Label"))
			GameObject.Find("Text - Detection Label").GetComponent<Text>().text = receivedKey + " up";

		if (OnExternalInputDidEndPressForKey != null)
			OnExternalInputDidEndPressForKey(receivedKeyCode);
	}

	public void DidChangePressForKey(string receivedKey)
	{
		// forceValue represents the force of an analog button being pressed in the range 0-1f
		// digital buttons return either 0 or 1

		KeyCode receivedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), receivedKey);

		print("press changed for key " + receivedKey);

		if (OnExternalInputDidChangePressForKey != null)
			OnExternalInputDidChangePressForKey(receivedKeyCode);
	}

	public void DidReceiveForceValue(float forceValue)
    {
		print("press generated a force with value: " + forceValue.ToString());

		if (OnExternalInputDidReceiveForceValue != null)
			OnExternalInputDidReceiveForceValue(forceValue);
    }

	public void DidCancelPressForKey(string receivedKey)
	{
		// the key press was cancelled by an iOS system event ex: low memory, removal of view handling the press, app becoming inactive, etc. and is an opportunity to do some clean up if necessary

		KeyCode receivedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), receivedKey);

		print("press cancelled for key " + receivedKey);

		if (OnExternalInputDidCancelPressForKey != null)
			OnExternalInputDidCancelPressForKey(receivedKeyCode);
	}
}
