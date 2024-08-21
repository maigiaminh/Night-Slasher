using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyStateMachine
{

    void Start()
    {
        InitializeFSM();
        ChangeState(new IdleState(this));
    }

    protected override void InitializeFSM()
    {
        attackeType = AttackType.RangedAttack;
        attackCooldown = 2.5f;
        damage = 50;
        moveSpeed = 2f;
        
        col = GetComponent<CapsuleCollider2D>();
        colDistance = 1.39f;
        range = 7f;

        retreatColDistance = 0.75f;
        retreatRange = 3f;

        animator = GetComponent<Animator>();

        playerLayer = LayerMask.GetMask("Ignore Raycast");
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        healthBar = GetComponent<HealthBar>();

        lastScale = transform.localScale.x;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * range * transform.localScale.x * colDistance,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y + 4, col.bounds.size.z));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * retreatRange * transform.localScale.x * retreatColDistance,
            new Vector3(col.bounds.size.x * retreatRange, col.bounds.size.y + 4, col.bounds.size.z));
          
    }
}
