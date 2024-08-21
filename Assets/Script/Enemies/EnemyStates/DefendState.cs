using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : BaseState
{
    private readonly int AtkFinishHash = Animator.StringToHash("Attack Finished");
    private readonly int AtkPrepHash = Animator.StringToHash("Attack Prep");
    private readonly int ShieldFinishHash = Animator.StringToHash("Shiled Finished");
    private readonly int ShieldPrepHash = Animator.StringToHash("Shield Prep");
    private readonly int ShieldPulseHash = Animator.StringToHash("Shield Pulse");

    private bool isActiveShield;
    private bool isAttacking;
    public DefendState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        stateMachine.isDoneAnimation = false;
        isActiveShield = false;
        isAttacking = false;
        Debug.Log("Enemy Enter Defend State.");
        
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.PlayerInSight() && !isAttacking){
            stateMachine.animator.Play(AtkPrepHash);
            isAttacking = true;
            return;
        }

        if(!stateMachine.PlayerInSight() && isAttacking){
            stateMachine.animator.Play(AtkFinishHash);
            isAttacking = false;
            return;
        }

        if(stateMachine.PlayerInDangerZone() && !isActiveShield){
            stateMachine.animator.Play(ShieldPrepHash);
            isActiveShield = true;
            return;
        }

        if(!stateMachine.PlayerInDangerZone() && isActiveShield){
            stateMachine.animator.Play(ShieldFinishHash);
            stateMachine.isImmortal = false;
            isActiveShield = false;
            return;
        } 
        
        if((!stateMachine.PlayerInSight() || !stateMachine.PlayerInDangerZone()) && stateMachine.isDoneAnimation){
            stateMachine.ChangeState(new ChaseState(stateMachine));
        }
    }

    public override void ExitState()
    {
        
    }

}
