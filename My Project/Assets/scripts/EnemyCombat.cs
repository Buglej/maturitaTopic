using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Animator animator;
    public Bandit isDead;
    public Bandit bandit;
    private Rigidbody2D body;
    private BoxCollider2D hitbox;
    private GameObject player;
    private banditAttackTrigger attackTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDead = GetComponent<Bandit>();
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        bandit = GetComponent<Bandit>();
        attackTrigger = GetComponentInChildren<banditAttackTrigger>();
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
        isDead.m_isDead = true;
        attackTrigger.Death();
        
        body.gravityScale = 0;
        hitbox.enabled = false;
        gameObject.layer = 0;
        animator.SetTrigger("Death");
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Wait for the length of the death animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        body.linearVelocity = Vector2.zero; // Set velocity to zero
        animator.enabled = false; // Disable the Animator component
        this.enabled = false; // Disable this script
    }

    void hurt()
    {
        animator.SetTrigger("Hurt");
        if (player.transform.position.x > transform.position.x)
        {
            bandit.knockback(-1);
        }
        else
        {
            bandit.knockback(1);
        }
    }
}

