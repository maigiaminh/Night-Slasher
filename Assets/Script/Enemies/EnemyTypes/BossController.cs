using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyStateMachine
{
    public static readonly int NormalAttackHash = Animator.StringToHash("Normal Attack");
    public static readonly int JumpAttackHash = Animator.StringToHash("Jump");
    public static readonly int Phase2NormalAttackHash = Animator.StringToHash("Phase 2 Normal Attack");
    public static readonly int Phase2Jump = Animator.StringToHash("Phase 2 Jump");
    public static readonly int SpitAcidHash = Animator.StringToHash("Spit Acid");

    public float verRange = 0;
    public int phase = 1;
    
    public bool isChangePhase;

    [Header ("Audio clip")]
    public AudioClip landClip;
    public AudioClip attackClip;
    public AudioClip spitClip;
    public AudioClip jumpClip;
    public AudioClip explodeClip;
    public AudioClip buffClip;

    protected override void InitializeFSM()
    {
        attackeType = AttackType.Boss;
        cooldownTimer = Mathf.Infinity;
        
        col = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();

        playerLayer = LayerMask.GetMask("Ignore Raycast");
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        healthBar = GetComponent<HealthBar>();

        lastScale = transform.localScale.x;
        phase = 1;
        isChangePhase = false;
    }

    void Start()
    {   
        InitializeFSM();
        ChangeState(new BossWakeUpState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * range * transform.localScale.x * colDistance + Vector3.down * verRange,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y / 2 , col.bounds.size.z)); 

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * retreatRange * transform.localScale.x * retreatColDistance + Vector3.down * verRange,
            new Vector3(col.bounds.size.x * retreatRange, col.bounds.size.y / 2, col.bounds.size.z));
    }

    public bool PlayerInJumpingZone(){
        RaycastHit2D hit = 
            Physics2D.BoxCast(col.bounds.center + transform.right * retreatRange * transform.localScale.x * retreatColDistance,
            new Vector3(col.bounds.size.x * retreatRange, col.bounds.size.y, col.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

            return hit.collider != null;
    }

}
