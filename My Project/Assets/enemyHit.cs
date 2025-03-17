using UnityEngine;

public class enemyHit : MonoBehaviour
{
    public Movement movement;
    private Transform transform;
    private Transform enemyLocation = null;
    public playerCombat playerCombat;
    private playerCombat health;
    void Start()
    {
        health = GetComponent<playerCombat>();
        movement = GameObject.Find("Player").GetComponent<Movement>();
        transform = GetComponent<Transform>();
        playerCombat = GameObject.Find("Player").GetComponent<playerCombat>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            enemyLocation = other.GetComponent<Transform>();
            if (enemyLocation.position.x > transform.position.x)
            {
                movement.knockback(-1);
            }
            else
            {
                movement.knockback(1);
            }
            playerCombat.TakeDamage(1);
        }
    }
}
