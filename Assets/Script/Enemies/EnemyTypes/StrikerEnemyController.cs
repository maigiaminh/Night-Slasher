using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerEnemyController : EnemyStateMachine
{
    protected override void InitializeFSM()
    {
        attackeType = AttackType.LightAttack;
        attackCooldown = 2f;
        range = 1.75f;
        damage = 5;
        moveSpeed = 5f;
        cooldownTimer = Mathf.Infinity;
        
        col = GetComponent<CapsuleCollider2D>();
        colDistance = 1f;

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
