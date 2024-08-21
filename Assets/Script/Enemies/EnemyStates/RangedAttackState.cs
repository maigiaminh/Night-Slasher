using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : BaseState
{
    private readonly int AimingHash = Animator.StringToHash("Charge");
    private Transform firePoint;
    private EnemyBulletPool pool;

    public RangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void EnterState()
    {
        stateMachine.animator.Play(AimingHash);
        firePoint = stateMachine.transform.Find("BulletPool");
        pool = firePoint.GetComponent<EnemyBulletPool>();
        stateMachine.isDoneAnimation = false;
        Debug.Log("Enemy Enter Ranged Attack State.");
        
    }

    public override void UpdateState(float deltaTime)
    {
        if(stateMachine.isDoneAnimation || !stateMachine.PlayerInSight() || stateMachine.PlayerInDangerZone()){
            stateMachine.ChangeState(new ChaseState(stateMachine));
        }
        else{
            if(stateMachine.cooldownTimer < stateMachine.attackCooldown){
                stateMachine.cooldownTimer += deltaTime;
            }

            if(stateMachine.cooldownTimer > stateMachine.attackCooldown){
                stateMachine.cooldownTimer = 0f;
                stateMachine.animator.Play(stateMachine.AttackHash);
                Shoot();
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exit Heavy Attack State.");
    }

    // private void Shoot(){
    //     Debug.Log("BUM");
    //     EnemyProjectile projectile = pool.FindActiveBullet()?.GetComponent<EnemyProjectile>();
    //     projectile.transform.position = firePoint.transform.position;
    //     projectile.transform.localScale = stateMachine.transform.localScale;
    //     projectile.ActivateProjectile();


    // }
    private void Shoot()
    {
        Debug.Log("BUM");

        EnemyProjectile projectile = pool.FindActiveBullet()?.GetComponent<EnemyProjectile>();

        if (projectile != null)
        {
            projectile.transform.position = firePoint.transform.position;
            projectile.transform.localScale = stateMachine.transform.localScale;

            projectile.ActivateProjectile();

            // RaycastHit2D hit = Physics2D.BoxCast(
            //     stateMachine.col.bounds.center + stateMachine.transform.right * stateMachine.range * stateMachine.transform.localScale.x * stateMachine.colDistance,
            //     new Vector3(stateMachine.col.bounds.size.x * stateMachine.range, stateMachine.col.bounds.size.y, stateMachine.col.bounds.size.z),
            //     0, Vector2.left, 0, stateMachine.playerLayer
            // );

            // if (hit.collider != null)
            // {
            //     Debug.Log("Hit collider: " + hit.collider.gameObject.name + ", layer: " + hit.collider.gameObject.layer);

            //     HealthBar playerController = hit.collider.GetComponent<HealthBar>();

            //     PlayerController player = hit.collider.GetComponent<PlayerController>();

            //     if (playerController != null && player != null)
            //     {
            //         float MaxHP = player.HP;
            //         float currentHealth = playerController.currentHealth;
            //         float newHealth = currentHealth - stateMachine.damage;

            //         playerController.SetHealth(newHealth);
            //         playerController.UpdateHealth(newHealth, MaxHP);
            //         Debug.Log("HP Player: " + newHealth);
            //     }
            // }
            // else
            // {
            //     Debug.Log("No collider hit.");
            // }

            // Debug.Log("Attacked");
        }
    }

}
