using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFocus : MonoBehaviour
{

    public CinemachineTargetGroup targetGroup;
    public CinemachineVirtualCamera virtualCamera;
    private PlayerInput input = new PlayerInput();
    private Players controls;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of camera group");
        input.onActionTriggered += Input_onActionTriggered;
        var players = GameObject.FindGameObjectsWithTag("Player");

        int i = 0;
        foreach (var p in players)
        {
            //CinemachineTargetGroup.Target target = p;
            targetGroup.m_Targets[i].target = p.transform; //SetValue(Convert.ChangeType(p, Type.GetType("CinemachineTargetGroup.Target")), i);
            Debug.Log($"Player{i + 1}: {p.name}");
            i++;

        }

        Debug.Log("End of start");
    }


    // Update is called once per frame
    void Update()
    {
        // change the follow on X based on the inputs from players
    }

    public void OnPlayerMove(InputAction.CallbackContext ctx)
    {
        var xAxis = ctx.ReadValue<float>();
        var followOffset = new Vector3(xAxis, 0f, 0f);
        virtualCamera.Follow.position = followOffset;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.Gamepad.Movement.name)
        {
            OnPlayerMove(obj);
        }
    }
}
