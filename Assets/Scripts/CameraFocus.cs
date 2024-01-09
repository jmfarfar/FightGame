using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{

    public CinemachineTargetGroup targetGroup;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of camera group");
        
        //targetGroup.m_Targets.SetValue();
        Debug.Log("End of start");
    }

    // Update is called once per frame
    void Update()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        int i = 0;
        foreach (var p in players)
        {
            Debug.Log($"Player{i}: {p.name}");
            i++;
        }
    }
}
