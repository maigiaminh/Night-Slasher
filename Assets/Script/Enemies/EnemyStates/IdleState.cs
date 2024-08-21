
using UnityEngine;

public class IdleState : BaseState
{

    private float idleDuration = 2f;

    public IdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }


    public override void EnterState()
    {
        Debug.Log("Enemy Enter Idle State.");
        stateMachine.isImmortal = true;
        stateMachine.animator.Play(stateMachine.IdleHash);
    }

    public override void UpdateState(float deltaTime)
    {

        idleDuration -= deltaTime;
        if (idleDuration <= 0f || stateMachine.isDoneAnimation)
        {
            stateMachine.isDoneAnimation = false;
            stateMachine.isImmortal = false;
            stateMachine.ChangeState(new ChaseState(stateMachine));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exit Idle State.");
    }

}
