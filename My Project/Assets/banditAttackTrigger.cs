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
    
    void Start()
    {
        m_body2d = GameObject.Find("HeavyBandit").GetComponent<Rigidbody2D>();
        playerCombat = GameObject.Find("Player").GetComponent<playerCombat>();
        bandit = GameObject.Find("HeavyBandit").GetComponent<Bandit>();
        animator = GameObject.Find("HeavyBandit").GetComponent<Animator>();
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
            m_body2d.linearVelocity = new Vector2(0, 0);
            animator.SetTrigger("Attack");
            StartCoroutine(DelayedDamage(0.5f)); // Adjust the delay time as needed
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
        }
        else
        {
            return;
        }
    }

    private IEnumerator DelayedDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerInRange)
        {
            playerCombat.TakeDamage(1);
            bandit.Attack();
        }
    }
}
