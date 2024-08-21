using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyController : EnemyStateMachine
{

    protected override void InitializeFSM()
    {
        attackeType = AttackType.Special;
        cooldownTimer = Mathf.Infinity;
        
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        playerLayer = LayerMask.GetMask("Ignore Raycast");
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        lastScale = transform.localScale.x;

    }

    void Start()
    {   
        InitializeFSM();
        ChangeState(new SpecialState(this));
    }
}
