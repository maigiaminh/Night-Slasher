using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAttackState : BaseState
{
    private bool isJumping;
    public JumpingAttackState(BossController stateMachine) : base(stateMachine) { }

    BossController controller = new BossController();
    public override void EnterState()
    {
        Debug.Log("Boss Enter Jump Attack State.");
        controller = (BossController) stateMachine;
        controller.otherSource.clip = controller.jumpClip;
        stateMachine.animator.Play(BossController.JumpAttackHash);
        stateMachine.isDoneAnimation = false;
        isJumping = true;
    }

    public override void UpdateState(float deltaTime)
    {
        if(isJumping && stateMachine.isDoneAnimation){
            isJumping = false;
            stateMachine.isDoneAnimation = false;
            controller.otherSource.clip = controller.landClip;
        }

        if(!isJumping && stateMachine.isDoneAnimation){
            stateMachine.ChangeState(new BossChaseState((BossController) stateMachine));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Boss Exit Jump Attack State.");
    }

}
