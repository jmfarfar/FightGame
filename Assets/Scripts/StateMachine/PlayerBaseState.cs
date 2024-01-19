using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : Base
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
