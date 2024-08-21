using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyController : EnemyStateMachine
{

    protected override void InitializeFSM()
    {
        attackeType = AttackType.HeavyAttack;
        cooldownTimer = Mathf.Infinity;
        
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
}
