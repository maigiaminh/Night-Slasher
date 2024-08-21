using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : EnemyState
{
    protected EnemyStateMachine stateMachine;

    public BaseState(EnemyStateMachine stateMachine){
        this.stateMachine = stateMachine;
    }
    
    protected void Chase(float deltaTime){
        Rotate();
        Vector3 direction = (stateMachine.playerTransform.position - stateMachine.transform.position).normalized;
        direction.y = 0;
        direction.z = 0;
        //Debug.Log("Chasing: " + direction);
        stateMachine.transform.Translate(direction * stateMachine.moveSpeed * deltaTime);
    }

    protected void Retreat(float deltaTime){
        Vector3 direction =  stateMachine.transform.localScale * -1;
        direction.y = 0;
        direction.z = 0;

        Debug.Log("Retreat: " + direction);
        stateMachine.transform.Translate(direction * stateMachine.moveSpeed * deltaTime);
    }

    private void Rotate(){
        Vector3 scale = stateMachine.transform.localScale;
        if(stateMachine.attackeType == AttackType.Boss){
            if(stateMachine.playerTransform.position.x > stateMachine.transform.position.x){
                scale.x = Mathf.Abs(scale.x);
            }
            else{
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            if(scale.x != stateMachine.lastScale){
                stateMachine.transform.localScale = scale;
                stateMachine.lastScale = scale.x;

                Vector3 newPos = stateMachine.transform.position;
                newPos.x = scale.x == -0.75f ? (newPos.x - EnemyBoundary.getBoundary(stateMachine.enemyType)) 
                                        : (newPos.x + EnemyBoundary.getBoundary(stateMachine.enemyType));

                stateMachine.transform.position = newPos;
            }
        }
        else{
            if(stateMachine.playerTransform.position.x > stateMachine.transform.position.x){
            scale.x = Mathf.Abs(scale.x);
            }
            else{
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            if(scale.x != stateMachine.lastScale){
                stateMachine.transform.localScale = scale;
                stateMachine.lastScale = scale.x;

                Vector3 newPos = stateMachine.transform.position;
                newPos.x = scale.x == -1 ? (newPos.x - EnemyBoundary.getBoundary(stateMachine.enemyType)) 
                                        : (newPos.x + EnemyBoundary.getBoundary(stateMachine.enemyType));

                stateMachine.transform.position = newPos;
            }
        }
    }
}
