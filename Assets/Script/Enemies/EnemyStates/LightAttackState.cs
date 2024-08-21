using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackState : BaseState
{
    private bool isPlayingAnimation;


    public LightAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enemy Enter Light Attack State.");
        stateMachine.animator.Play(stateMachine.IdleHash);
        stateMachine.isDoneAnimation = false;
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.PlayerInSight() && !isPlayingAnimation){
            if(stateMachine.cooldownTimer < stateMachine.attackCooldown){
                stateMachine.cooldownTimer += deltaTime;
            }

            if(stateMachine.cooldownTimer >= stateMachine.attackCooldown){
                stateMachine.cooldownTimer = 0f;
                stateMachine.animator.Play(stateMachine.AttackHash);
                isPlayingAnimation = true;
            }
        }

        if(!stateMachine.PlayerInSight() && !isPlayingAnimation){
            stateMachine.ChangeState(new ChaseState(stateMachine));
        }

        if(stateMachine.isDoneAnimation){
            isPlayingAnimation = false;
            stateMachine.isDoneAnimation = false;
            stateMachine.animator.Play(stateMachine.IdleHash);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exit Light Attack State.");
    }

}
