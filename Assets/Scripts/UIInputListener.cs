using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInputListener : MonoBehaviour, Players.IUIActions
{
    public UnityEvent backButtonPress;
    public UnityEvent acceptButtonPress;
    public UnityEvent startButtonPress;
    public UnityEvent LeftTriggerButtonPress;
    public UnityEvent rightTriggerButtonPress;
    private Players controls;

    private void Awake()
    {
        controls = new Players();
        controls.UI.Enable();
        controls.UI.SetCallbacks(this);
    }

    public void OnAceptar(InputAction.CallbackContext context)
    {

        acceptButtonPress?.Invoke();
    }

    public void OnRegresar(InputAction.CallbackContext context)
    {
        backButtonPress?.Invoke();
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        startButtonPress?.Invoke();
    }

    public void OnLeftSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            LeftTriggerButtonPress?.Invoke();

        }
    }

    public void OnRightSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rightTriggerButtonPress?.Invoke();

        }
    }
}
