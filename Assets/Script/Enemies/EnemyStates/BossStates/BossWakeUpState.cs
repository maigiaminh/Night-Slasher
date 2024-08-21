
using UnityEngine;

public class BossWakeUpState : BaseState
{
    private readonly int WakeUpHash = Animator.StringToHash("WakeUp");
    private float wakeupTime = 2f;
    BossController controller = new BossController();


    public BossWakeUpState(BossController stateMachine) : base(stateMachine) { }


    public override void EnterState()
    {
        Debug.Log("Boss Enter Wakup State.");
        controller = (BossController) stateMachine;
        controller.otherSource.clip = controller.landClip;
        stateMachine.isImmortal = true;
        stateMachine.animator.Play(WakeUpHash);
    }

    public override void UpdateState(float deltaTime)
    {

        wakeupTime -= deltaTime;
        if (wakeupTime <= 0f || stateMachine.isDoneAnimation)
        {
            stateMachine.isDoneAnimation = false;
            stateMachine.isImmortal = false;
            stateMachine.ChangeState(new BossChaseState((BossController) stateMachine));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Boss Exit Idle State.");
    }

}
