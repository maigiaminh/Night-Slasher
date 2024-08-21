using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpecialState : BaseState
{
    public SpecialState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enemy Enter Special State.");
        stateMachine.isImmortal = true;
        if(stateMachine.enemyType == EnemyType.AncientLaser){
            Vector3 offset = (stateMachine.playerTransform.localScale.x == 1) ?  new Vector3(2, -2, 0) : new Vector3(-2, -2, 0);
            stateMachine.transform.position = stateMachine.playerTransform.position + offset;
            stateMachine.transform.localScale = stateMachine.playerTransform.localScale;
        }

        stateMachine.animator.Play(stateMachine.IdleHash);
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.PlayerInSight() && stateMachine.enemyType == EnemyType.BabyBoxer){
            stateMachine.animator.Play(stateMachine.AttackHash);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exit Special State.");
    }
}
