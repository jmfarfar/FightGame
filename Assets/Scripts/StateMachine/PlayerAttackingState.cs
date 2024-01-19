using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{

    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack attack;

    private const float CrossFadeDuration = 0.1f;    

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base (stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.MagicDamage.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
      //  Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if(stateMachine.PlayerInputHandler.isAttacking)
            {
                if (stateMachine.PlayerInputHandler.attackIndex == 0)
                {
                    TryComboAttack(normalizedTime);
                }
            }
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }



    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAtackTime) { return; }

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )   
        );
    }


    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }

}
