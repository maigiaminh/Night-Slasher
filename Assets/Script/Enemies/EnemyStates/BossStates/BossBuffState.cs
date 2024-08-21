
using UnityEngine;

public class BossBuffState : BaseState
{
    private readonly int BuffHash = Animator.StringToHash("Buff");
    private float buffTime = 2f;
    public BossBuffState(BossController stateMachine) : base(stateMachine) { }

    BossController controller = new BossController();

    public override void EnterState()
    {
        Debug.Log("Boss Enter Buff State.");
        stateMachine.isDoneAnimation = false;
        stateMachine.isImmortal = true;
        stateMachine.animator.Play(BuffHash);
        controller = (BossController) stateMachine;
        controller.otherSource.clip = controller.buffClip;
    }

    public override void UpdateState(float deltaTime)
    {

        buffTime -= deltaTime;
        if (buffTime <= 0f || stateMachine.isDoneAnimation)
        {
            stateMachine.isDoneAnimation = false;
            stateMachine.isImmortal = false;
            controller.isChangePhase = true;
            stateMachine.animator.speed = 1.25f;
            stateMachine.ChangeState(new BossChaseState((BossController) stateMachine));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Boss Exit Buff State.");
    }

}
