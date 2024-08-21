using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; 

public enum EnemyType{
    HammerGuy,
    ToasterBot,
    RavenShadowShooter,
    BallnChain,
    StormHead,
    DroidZapper,
    PoliceShielder,
    AncientLaser,
    BabyBoxer,
    TheTarnishedWidow
}

public enum AttackType{
    None,
    HeavyAttack,
    LightAttack,
    RangedAttack,
    Defend,
    Special,
    Boss
}

public abstract class EnemyStateMachine : FSMSystem

{
    [Header ("Enemy Type")]
    public EnemyType enemyType;

    [Header ("Attack Parameters")]

    public AttackType attackeType;
    public float attackCooldown;
    public int damage;
    public float moveSpeed;
    public float cooldownTimer;


    [Header("Collider Parameters")]
    public Collider2D col;
    public float colDistance;
    public float range;
    public float retreatColDistance;
    public float retreatRange;


    [Header("Player")]
    public LayerMask playerLayer;
    public Transform playerTransform;
    public GameObject playerCamera;

    [Header("Animator")]
    public Animator animator;
    [Header("Audio")]
    public AudioSource attackSource;
    public AudioSource otherSource;

    public readonly int IdleHash = Animator.StringToHash("Idle");
    public readonly int AttackHash = Animator.StringToHash("Attack");
    public readonly int RunHash = Animator.StringToHash("Run");
    public readonly int HitHash = Animator.StringToHash("Hit");
    public readonly int DeathHash = Animator.StringToHash("Death");

    [Header("Others")]
    public HealthBar healthBar;
    public bool isDoneAnimation = false;
    public bool isImmortal = false;
    public float lastScale;
  private void Start(){
        ChangeState(new IdleState(this));
    }

    protected abstract void InitializeFSM();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * range * transform.localScale.x * colDistance,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z));
    }

    private void FinishAnimation(){
        isDoneAnimation = true;
        Debug.Log("FinishAnimation");
    }

    private void AttackSound(){
        attackSource.Play();
    }
    private void StopAttackSound(){
        attackSource.Stop();
    }

    private void OtherActiveSound(){
        otherSource.Play();
    }

    private void StopOtherSound(){
        otherSource.Stop();
    }

    private void ShakeCamera(){
        playerCamera.GetComponent<CameraBehavior>().ShakeCamera();
    }

    private void FollowPlayer(){
        if(attackeType == AttackType.Boss){
            transform.position = playerTransform.position + new Vector3(0, -11, 0);
        }
    }

    private void Death(){
        Destroy(gameObject, 4f);
        Destroy(this);
        if(transform.childCount > 0){
            transform.GetChild(0).gameObject.SetActive(false);
        }
        Debug.Log("Death");
    }

    private void SetImortal(){
        isImmortal = true;
    }

    private void Chase(){
        ChangeState(new ChaseState(this));
    }
    public bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(col.bounds.center + transform.right * range * transform.localScale.x * colDistance,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

            return hit.collider != null;
    }

    public bool PlayerInDangerZone()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(col.bounds.center + transform.right * retreatRange * transform.localScale.x * retreatColDistance,
            new Vector3(col.bounds.size.x * retreatRange, col.bounds.size.y, col.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

            return hit.collider != null;
    }

    private void AttackPlayer()
    {
        if (PlayerInSight())
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                col.bounds.center + transform.right * range * transform.localScale.x * colDistance,
                new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z),
                0, Vector2.left, 0, playerLayer
            );
            
            if (hit.collider != null)
            {
                Debug.Log("Hit collider: " + hit.collider.gameObject.name + ", layer: " + hit.collider.gameObject.layer);

                HealthBar playerController = hit.collider.GetComponent<HealthBar>();
                PlayerController player = playerController.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    float MaxHP = player.HP;
                    float currentHealth = playerController.currentHealth;
                    float newHealth = currentHealth - damage;

                    playerController.SetHealth(newHealth);
                    playerController.UpdateHealth(newHealth, MaxHP);
                    Debug.Log("HP Player: " + newHealth);                   
                }
            }
            else
            {
                Debug.Log("No collider hit.");
            }

            Debug.Log("Attacked");
        }
    }
    
}

public static class EnemyBoundary
{
    public static readonly float HammerGuy = 1.72f;
    public static readonly float ToasterBot = 6.79f;
    public static readonly float RavenShadowShooter = 0.47f;
    public static readonly float BallnChain = 1.35f;
    public static readonly float StormHead = 0;
    public static readonly float DroidZapper = 0.32f;
    public static readonly float PoliceShielder = 2f;


    public static float getBoundary(EnemyType type){
        switch(type){
            case EnemyType.HammerGuy:
                return HammerGuy;
            case EnemyType.ToasterBot:
                return ToasterBot;
            case EnemyType.RavenShadowShooter:
                return RavenShadowShooter;
            case EnemyType.BallnChain:
                return BallnChain;
            case EnemyType.StormHead:
                return StormHead;
            case EnemyType.DroidZapper:
                return DroidZapper;
            case EnemyType.PoliceShielder:
                return PoliceShielder;
            default:
                return 0;
        }
    }
}

