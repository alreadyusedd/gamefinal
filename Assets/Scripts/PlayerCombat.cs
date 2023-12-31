using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    [SerializeField] private FixedJoystick _joystick;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    
    
    
    // Update is called once per frame
    void Update()
    {   
      if(Time.time >= nextAttackTime)
      {
        if (Input.GetKeyDown(KeyCode.F))
        {
          Attack();
          
        }

        
      }
    }
    
    public void Attack() 
    {
     animator.SetTrigger("Attack");
     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

          foreach(Collider2D enemy in hitEnemies)
          {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
          }
      nextAttackTime = Time.time + 1f / attackRate;
    }

     void OnDrawGizmosSelected()
  {
    if (attackPoint == null)
      return;

    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }
    

  
}
