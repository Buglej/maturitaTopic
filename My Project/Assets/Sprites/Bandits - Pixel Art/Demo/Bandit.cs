using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] private LayerMask playerLayer;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    public bool m_isDead = false;
    public bool aggro = false;
    private int moveDirection = 0;
    private GameObject player;
    private Movement movement;
    private BoxCollider2D dmgRange;
    private CapsuleCollider2D hitbox;
    [SerializeField] public float attackCooldown;
    [SerializeField] private float attackRange;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.5f; // Duration of the knockback effect
    private float knockbackTimer = 0f;

    // Use this for initialization
    void Start () {
        dmgRange = GetComponent<BoxCollider2D>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        movement = GameObject.Find("Player").GetComponent<Movement>();
        player = GameObject.Find("Player");
        hitbox = transform.Find("PlayerSensor").GetComponent<CapsuleCollider2D>();
    }
    
    // Update is called once per frame
    void Update () {
        if (isKnockedBack) {
            knockbackTimer += Time.deltaTime;
            attackCooldown += Time.deltaTime;
            if (knockbackTimer >= knockbackDuration) {
                isKnockedBack = false;
                knockbackTimer = 0f;
            }
        } else {
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            if (aggro && player.GetComponent<playerCombat>().currentHealth > 0)
            {
                if (distanceToPlayer <= attackRange && attackCooldown > 0.9f)
                {
                    moveDirection = 0; // Stop moving when in range of attack
                    m_animator.SetTrigger("Attack");
                }
                else if (player.transform.position.x > transform.position.x && attackCooldown > 0.9f)
                {
                    moveDirection = 1;
                }
                else if (player.transform.position.x < transform.position.x && attackCooldown > 0.9f)
                {
                    moveDirection = -1;
                }
                else
                {
                    moveDirection = 0;
                    attackCooldown += Time.deltaTime;
                }
            }
            else if (player.GetComponent<playerCombat>().currentHealth <= 0)
            {
                moveDirection = 0;
                hitbox.enabled = false;
            }
            else
            {
                moveDirection = 0;
            }

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State()) {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if(m_grounded && !m_groundSensor.State()) {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // Swap direction of sprite depending on walk direction
            if (m_body2d.linearVelocity.x > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (m_body2d.linearVelocity.x < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            m_body2d.linearVelocity = new Vector2(moveDirection * m_speed, m_body2d.linearVelocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeed", m_body2d.linearVelocity.y);

            // -- Handle Animations --
            //Death
            if (m_isDead)
            {
                this.enabled = false;
            }
                
            //Run
            else if (Mathf.Abs(moveDirection) > Mathf.Epsilon)
                m_animator.SetInteger("AnimState", 2);

            //Combat Idle
            else if (m_combatIdle)
                m_animator.SetInteger("AnimState", 1);

            //Idle
            else
                m_animator.SetInteger("AnimState", 0);
        }
    }

    public void Attack()
    {
        Debug.Log("Attack called");
        if (player.transform.position.x > transform.position.x)
        {
            movement.knockback(1);
        }
        else
        {
            movement.knockback(-1);
        }
    }

    public void knockback(int direction)
    {
        Debug.Log("Knockback called with direction: " + direction);
        attackCooldown = 0;
        m_animator.SetTrigger("Hurt");
        m_body2d.linearVelocity = new Vector2(5 * direction, 10);
        isKnockedBack = true;
        knockbackTimer = 0f;
    }
}
