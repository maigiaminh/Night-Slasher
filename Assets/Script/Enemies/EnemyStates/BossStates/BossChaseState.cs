using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossChaseState : BaseState
{

    public BossChaseState(BossController stateMachine) : base(stateMachine) { }
    BossController controller = new BossController();

    public override void EnterState()
    {
        Debug.Log("Enemy Enter Chasing Player State.");
        stateMachine.animator.Play(stateMachine.RunHash);
        controller = (BossController) stateMachine;
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.healthBar.currentHealth <= 5000 && controller.phase != 2){
            controller.phase = 2;
        }

        if(!controller.isChangePhase && controller.phase == 2){
            stateMachine.ChangeState(new BossBuffState((BossController) stateMachine));
        }

        if(stateMachine.PlayerInSight()){
            if(controller.phase == 1){
                stateMachine.ChangeState(new NormalAttackState((BossController) stateMachine));
            }   
            else if (controller.phase == 2 && controller.isChangePhase){
                int randomAttack = Random.Range(0, 10);
                if(randomAttack <= 5){
                    stateMachine.ChangeState(new NormalAttackState((BossController) stateMachine));
                }
                else{
                    stateMachine.ChangeState(new SpitAcidState((BossController) stateMachine));
                }
            }
        }

        else if(controller.PlayerInJumpingZone()){
                stateMachine.ChangeState(new JumpingAttackState((BossController) stateMachine));
        } 

        else{
            Chase(deltaTime);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exit Chasing Player State.");
    }

}
