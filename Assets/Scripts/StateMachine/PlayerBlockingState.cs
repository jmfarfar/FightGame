using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{

    private readonly int BlockHash = Animator.StringToHash("block");

    private const float CrossFadeDuration = 0.1f;

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) 
    { 
    }


    public override void Enter()
    {
        stateMachine.Health.SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        if(!stateMachine.PlayerInputHandler.isBlocking)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }        
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

}
