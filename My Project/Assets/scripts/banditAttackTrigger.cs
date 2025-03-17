using UnityEngine;
using System.Collections;

public class banditAttackTrigger : MonoBehaviour
{
    private Bandit bandit;
    private playerCombat playerCombat;
    public LayerMask playerLayer;
    private Animator animator;
    private Rigidbody2D m_body2d;
    private bool playerInRange = false;
    private bool damageApplied = false; // Flag to check if damage has been applied

    void Start()
    {
        m_body2d = GetComponentInParent<Rigidbody2D>();
        playerCombat = GameObject.Find("Player").GetComponent<playerCombat>();
        bandit = GetComponentInParent<Bandit>();
        animator = GetComponentInParent<Animator>();
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
            bandit.attackCooldown = 0;
            m_body2d.linearVelocity = Vector2.zero;
            animator.SetTrigger("Attack");
            if (!damageApplied) // Check if damage has already been applied
            {
                StartCoroutine(DelayedDamage(0.5f)); // Adjust the delay time as needed
            }
        }
        else
        {
            return;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = false;
            damageApplied = false; // Reset the damage flag when the player exits the trigger
        }
        else
        {
            return;
        }
    }

    private IEnumerator DelayedDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerInRange && !damageApplied) // Check if the player is still in range and damage has not been applied
        {
            playerCombat.TakeDamage(1);
            bandit.Attack();
            damageApplied = true; // Set the damage flag to true
        }
    }
}
