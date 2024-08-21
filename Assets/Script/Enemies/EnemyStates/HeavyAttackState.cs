using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackState : BaseState
{
    public HeavyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private bool isPlayingAnimation;
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Heavy Attack State.");
        stateMachine.animator.Play(stateMachine.IdleHash);
        stateMachine.isDoneAnimation = false;
        isPlayingAnimation = false;
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.isDoneAnimation && stateMachine.enemyType == EnemyType.HammerGuy){
            float xDeviation = stateMachine.transform.position.x  + (stateMachine.transform.localScale.x * -1.4f);
            Vector3 newPos = new Vector3(xDeviation, stateMachine.transform.position.y, stateMachine.transform.position.z);
            stateMachine.transform.position = newPos;
        }
        
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
        Debug.Log("Enemy Exit Heavy Attack State.");
    }

    
}
