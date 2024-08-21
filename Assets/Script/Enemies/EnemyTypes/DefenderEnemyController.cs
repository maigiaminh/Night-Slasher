using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderEnemyController : EnemyStateMachine
{
    protected override void InitializeFSM()
    {
        attackeType = AttackType.Defend;
        attackCooldown = 2.5f;
        damage = 5;
        moveSpeed = 2f;
        
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        playerLayer = LayerMask.GetMask("Ignore Raycast");
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        healthBar = GetComponent<HealthBar>();

        lastScale = transform.localScale.x;
    }

    void Start()
    {   
        InitializeFSM();
        ChangeState(new IdleState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * range * transform.localScale.x * colDistance,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * retreatRange * transform.localScale.x * retreatColDistance,
            new Vector3(col.bounds.size.x * retreatRange, col.bounds.size.y, col.bounds.size.z));
          
    }
}
