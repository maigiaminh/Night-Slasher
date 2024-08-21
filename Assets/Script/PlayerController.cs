using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
  public GameObject gameOverMenu;
  public GameObject GameOverText;
  public GameObject pressAnyKeyText;
  public HealthBar PlayerHealthBar;
  public DashGhostEffect dashGhostEffect;
  public Animator animator;
  public float HP;
  public float DMG;
  public float runSpeed;
  public float jumpPower;
  public float dashPower;
  public float extraPower;
  public float dashDurationFrame;
  public float attackForce;
  public float attackTime;
  public float attackCooldown;
  public GameObject playerCamera;
  public AudioSource footstepAudio;
  public AudioSource slashAudio;
  public AudioSource dashAudio;
  public GameController gameController;

  private bool isGrounded = true;
  private bool isHitWallEast = false;
  private bool isHitWallWest = false;
  private bool isWallJump = false;
  private bool isAttacking = false;
  private bool isAbleAttack = true;

  private float inDash;
  private Rigidbody2D rb;
  private float orientation = 1;
  private Vector3 attackDirection;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    inDash = dashDurationFrame;
  }

  void Update()
  {
    if (Time.timeScale == 0) {
      footstepAudio.enabled = false;
      return;
    }
    if (isWallJump || isAttacking)
    {
      footstepAudio.enabled = false;
      if (isAttacking)
      {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position + attackDirection * 0.5f, 1.5f);
        foreach (var hit in hits)
        {
          if (hit.CompareTag("Enemy"))
          {
            EnemyStateMachine enemyStateMachine = hit.GetComponent<EnemyStateMachine>();
            HealthBar enemyController = hit.GetComponent<HealthBar>();
            if (enemyController != null && enemyController.currentHealth > 0)
            {
              float MaxHP = enemyController.health;
              float currentHealth;
              if (enemyController.currentHealth == MaxHP)
              {
                currentHealth = MaxHP;
              }
              else
              {
                currentHealth = enemyController.currentHealth;
              }
              float newHealth = currentHealth - DMG;

              enemyController.SetHealth(newHealth);
              enemyController.UpdateHealth(newHealth, MaxHP);
              Debug.Log("HP enemy: " + newHealth);
              enemyStateMachine.animator.Play(enemyStateMachine.HitHash);
              if (newHealth <= 0)
              {
                enemyStateMachine.animator.Play(enemyStateMachine.DeathHash);
                if (hit.gameObject.name.Contains("TheTarnishedWidow"))
                {
                  Invoke("EndingGame", 2f);
                }
                Debug.Log("Destroy enemy");
              }
              playerCamera.GetComponent<CameraBehavior>().ShakeCamera();
            }
          }
        }
      }
      isAttacking = false;
      //Change orientation
      transform.localScale = new Vector3(orientation, 1, 1);
      return;
    }
    float moveHorizontal = Input.GetAxis("Horizontal");

    if (isGrounded && isAbleAttack)
    {
      if (Mathf.Abs(moveHorizontal) != 0)
      {
        animator.Play("PlayerRun");
        footstepAudio.enabled = isGrounded;
      }
      else
      {
        animator.Play("PlayerIdle");
        footstepAudio.enabled = false;
      }
    }

    if (moveHorizontal > 0) orientation = 1;
    if (moveHorizontal < 0) orientation = -1;

    //Jumping
    if (isGrounded)
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        isGrounded = false;
        footstepAudio.enabled = false;
        rb.AddForce(new Vector2(rb.velocity.x, jumpPower), ForceMode2D.Impulse);
        if (!isAttacking) animator.Play("PlayerJump");
      }
    }
    else
    {
      if (Input.GetKey(KeyCode.Space) && inDash >= dashDurationFrame)
      {
        // dashGhostEffect.isDash = true;
        rb.AddForce(new Vector2(rb.velocity.x, extraPower * Time.deltaTime), ForceMode2D.Impulse);
      }
      else
      {
        // dashGhostEffect.isDash = false;
      }
    }

    //Walljumping
    if (isHitWallEast || isHitWallWest)
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        Debug.Log(new Vector2(-jumpPower * 3, jumpPower));
        rb.velocity = new Vector2(0, 0);
        if (isHitWallEast)
        {
          rb.AddForce(new Vector2(-jumpPower * 3, jumpPower), ForceMode2D.Impulse);
        }
        else
        {
          rb.AddForce(new Vector2(jumpPower * 3, jumpPower), ForceMode2D.Impulse);
        }
        orientation *= -1;
        isWallJump = true;
        Invoke("SetIsWallJumpFalse", 0.2f);
        return;
      }
    }

    //Dashing
    if (Input.GetKeyDown(KeyCode.LeftShift) && inDash >= dashDurationFrame)
    {
      dashGhostEffect.isDash = true;
      inDash = 0;
      dashAudio.Play();
    }
    if (inDash < dashDurationFrame)
    {
      // dashGhostEffect.isDash = true;
      rb.AddForce(new Vector2(moveHorizontal * dashPower, 0), ForceMode2D.Force);
      inDash += 1;
    }
    else
    {
      dashGhostEffect.isDash = false;
      rb.velocity = new Vector2(moveHorizontal * runSpeed, rb.velocity.y);
    }

    //Attacking
    if (Input.GetKeyDown(KeyCode.Mouse0) && isAbleAttack)
    {
      isAttacking = true;
      isAbleAttack = false;
      animator.Play("PlayerAttack");

      attackDirection = (new Vector2(Input.mousePosition.x - Screen.width / 2.0f, Input.mousePosition.y - Screen.height / 2.0f)
        - new Vector2(transform.position.x, transform.position.y)).normalized;

      if (attackDirection.x != 0) orientation = attackDirection.x / Mathf.Abs(attackDirection.x);
      rb.velocity = Vector2.zero;
      rb.AddForce(attackDirection * attackForce, ForceMode2D.Impulse);
      slashAudio.Play();
      Invoke("SetIsAttackingFalse", attackTime);
      Invoke("SetIsAbleAttack", attackCooldown);
      return;
    }

    attackDirection = (new Vector2(Input.mousePosition.x - Screen.width / 2.0f, Input.mousePosition.y - Screen.height / 2.0f)
       - new Vector2(transform.position.x, transform.position.y)).normalized;

    //Change orientation
    transform.localScale = new Vector3(orientation, 1, 1);

    // Set over


    if (PlayerHealthBar.currentHealth <= 0)
    {
      StartCoroutine(ShowGameOver());
    }
  }

  void SetIsWallJumpFalse()
  {
    isWallJump = false;
  }

  void SetIsAttackingFalse()
  {
    isAttacking = false;
    rb.velocity = Vector2.zero;

  }

  void SetIsAbleAttack()
  {
    isAbleAttack = true;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
      isGrounded = true;
    }
    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall East"))
    {
      isHitWallEast = true;
    }
    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall West"))
    {
      isHitWallWest = true;
    }
  }
  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall East"))
    {
      isHitWallEast = false;
    }
    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall West"))
    {
      isHitWallWest = false;
    }
  }

  IEnumerator ShowGameOver()
  {
    gameOverMenu.SetActive(true);
    yield return new WaitForSeconds(0.5f);

    GameOverText.SetActive(true);
    yield return new WaitForSeconds(3f);

    pressAnyKeyText.SetActive(true);

    if (Input.anyKeyDown)
    {
      RestartGame();
    }
  }

  void RestartGame()
  {
    SceneManager.LoadScene("Menu");
  }

  void EndingGame() {
    gameController.EndGame();
  }
} 
