using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitAcidState : BaseState
{
    public SpitAcidState(BossController stateMachine) : base(stateMachine) { }
    BossController controller = new BossController();

    public override void EnterState()
    {
        Debug.Log("Boss Enter Spit Acid Attack State.");
        controller = (BossController) stateMachine;
        controller.attackSource.clip = controller.spitClip;
        stateMachine.animator.Play(BossController.SpitAcidHash);
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
        Debug.Log("Boss Exit Spit Acid State.");
    }

}
