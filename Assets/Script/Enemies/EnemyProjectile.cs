using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private readonly int FireHash = Animator.StringToHash("Fire");
    private readonly int ExplosionHash = Animator.StringToHash("Explosion");
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rigid;
    private Transform player;
    private ParticleSystem particle;
    private float damage = 50;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        particle = GameObject.Find("Prefab_Explosion")?.GetComponent<ParticleSystem>();
    }

    public void ActivateProjectile()
    {
        anim.Play(FireHash);
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.Normalize();
        
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.rotation = quaternion;
        rigid.velocity = transform.right * speed;
    }
    private void Update()
    {
        if (hit) {
            return;
        }

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Projectile" && collision.tag != "Enemy"){
            rigid.velocity = Vector3.zero;
            hit = true;
            coll.enabled = false;

            if(collision.tag == "Player"){
                 HealthBar playerController = collision.GetComponent<HealthBar>();

                PlayerController player = collision.GetComponent<PlayerController>();

                if (playerController != null && player != null)
                {
                    float MaxHP = player.HP;
                    float currentHealth = playerController.currentHealth;
                    float newHealth = currentHealth - damage;

                    playerController.SetHealth(newHealth);
                    playerController.UpdateHealth(newHealth, MaxHP);
                    Debug.Log("HP Player: " + newHealth);
                }
            }
            if (anim != null){
                anim.Play(ExplosionHash); 
                particle.Play();
            }
            else{
                gameObject.SetActive(false); 
            }
        }
        
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
