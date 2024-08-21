using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackState : BaseState
{
    public NormalAttackState(BossController stateMachine) : base(stateMachine) { }
    BossController controller = new BossController();

    public override void EnterState()
    {
        Debug.Log("Boss Enter Normal Attack State.");
        controller = (BossController) stateMachine;
        controller.attackSource.clip = controller.attackClip;
        if(controller.phase == 1){
            stateMachine.animator.Play(BossController.NormalAttackHash);
        }
        else if(controller.phase == 2){
            stateMachine.animator.Play(BossController.Phase2NormalAttackHash);
        }

        stateMachine.isDoneAnimation = false;
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.isDoneAnimation){
            stateMachine.ChangeState(new BossChaseState((BossController) stateMachine));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Boss Exit Normal Attack State.");
    }

}
