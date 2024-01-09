using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private Base currentState;

    public void SwitchState(Base newState)
    {
         currentState?.Exit();
         currentState = newState;
         currentState.Enter();
    }


    void Update()
    {
        currentState?.Tick(Time.deltaTime);         
    }
}
