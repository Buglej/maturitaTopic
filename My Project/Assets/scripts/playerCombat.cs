using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform Attack;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int maxHealth = 3;
    public int currentHealth;
    private Rigidbody2D rb;
    
    void Start()
    {
        currentHealth = maxHealth;
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }


    void attack()
    {
        animator.SetTrigger("Attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attack.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            die();
        }
        else
        {
            hurt();
        }
    }

    void die()
    {   
        animator.SetTrigger("Death");
        this.GetComponent<Movement>().enabled = false;
        this.enabled = false;
    }

    void hurt()
    {
        animator.SetTrigger("Hurt");
    }

    void OnDrawGizmosSelected()
    {
        if (Attack == null) return;
        Gizmos.DrawWireSphere(Attack.position, attackRange);
    }    
}

