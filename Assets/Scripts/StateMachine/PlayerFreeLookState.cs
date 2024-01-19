using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");    
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;    

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) 
    { 
    }
    

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.PlayerInputHandler.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, stateMachine.PlayerInputHandler.attackIndex));
            return;
        }

        if (stateMachine.PlayerInputHandler.isBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if (stateMachine.PlayerInputHandler.isDodging)
        {
            if (stateMachine.PlayerInputHandler.MovementValue == Vector2.zero) { return; }
            stateMachine.SwitchState(new PlayerDodgeState(stateMachine, stateMachine.PlayerInputHandler.MovementValue));
            return;
        }

        Vector3 movement = new Vector3();
        movement.x = stateMachine.PlayerInputHandler.MovementValue.x;
        movement.y = 0;
        movement.z = stateMachine.PlayerInputHandler.MovementValue.y;
        movement.Normalize();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
//        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.PlayerInputHandler.MovementValue == Vector2.zero) 
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {

    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

}
