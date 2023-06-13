using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Animator animator;
  [SerializeField] private LayerMask playerLayer;
  private CharacterController2D player;
  public Transform attackPoint;
  public float attackRange = 0.5f;
  public int attackDamage = 20;
  public float attackRate = 2f;
  float nextAttackTime = 0f;



  private EnemyPatrol enemyPatrol;
  public int maxHealth = 100;
  int currentHealth;
  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
    player = GameObject.FindObjectOfType<CharacterController2D>();
    enemyPatrol = GetComponentInParent<EnemyPatrol>();
  }

  public void TakeDamage(int damage)
  {
    currentHealth -= damage;

    animator.SetTrigger("Hurt");

    if (currentHealth <= 0)
    {
      Die();
    }
  }
  // Update is called once per frame

  private void Update()
  {
    if (PlayerInSight())
    {
      if (Time.time >= nextAttackTime)
      {
        Attack();

      }
    }
    if (enemyPatrol != null)
      enemyPatrol.enabled = !PlayerInSight();
  }
  bool PlayerInSight()
  {


    var hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
    return hitPlayer?.gameObject.GetComponent<CharacterController2D>();
  }

  void Attack()
  {
    animator.SetTrigger("Attack");

    //  Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

    player.GetComponent<CharacterController2D>().TakeDamage(attackDamage);
    Debug.Log("Player taking damage");
    nextAttackTime = Time.time + 1f / attackRate;

  }


  void OnDrawGizmosSelected()
  {
    if (attackPoint == null)
      return;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }


  void Die()
  {
    Debug.Log("Enemy died!");

    animator.SetBool("IsDead", true);

    GetComponent<Collider2D>().enabled = false;
    this.enabled = false;
    ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Key, amount = 1 });
    GetComponent<EnemyPatrol>().enabled = false;
  }
}
