using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : BaseState
{

    public ChaseState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enemy Enter Chasing Player State.");
        stateMachine.animator.Play(stateMachine.RunHash);
    }

    public override void UpdateState(float deltaTime)
    {
        stateMachine.cooldownTimer += deltaTime;
        if(stateMachine.PlayerInDangerZone() && stateMachine.enemyType == EnemyType.RavenShadowShooter){
            Retreat(deltaTime);
            return;
        }
        else if(stateMachine.PlayerInSight()){
            switch(stateMachine.attackeType){
                case AttackType.None:
                    break;
                case AttackType.HeavyAttack:
                    stateMachine.ChangeState(new HeavyAttackState(stateMachine));
                    break;
                case AttackType.LightAttack:
                    stateMachine.ChangeState(new LightAttackState(stateMachine));
                    break;
                case AttackType.RangedAttack:
                    stateMachine.ChangeState(new RangedAttackState(stateMachine));
                    break;
                case AttackType.Defend:
                    stateMachine.ChangeState(new DefendState(stateMachine));
                    break;
            } 
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
