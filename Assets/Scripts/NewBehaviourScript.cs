using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public InputAction action;

    void Awake()
    {
        action = new InputAction("Test");

        action.AddBinding("<Keyboard>/e")
            .WithInteraction("Press");

        action.performed += ctx =>
        {
            Debug.Log("!");
        };
    }

    void OnEnable()
    {
        action.Enable();
    }

    void OnDisable()
    {
        action.Disable();
    }

    void Update()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            Debug.Log("!!");
        }

        if (InputSystem.GetDevice<Keyboard>().eKey.isPressed)
        {
            Debug.Log("!!!");
        }
    }
}
