using UnityEngine;

public class Movement : MonoBehaviour
{
    public CollManagement collection;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask cornerLayer;
    [SerializeField] private float knockbackForce = 5f; // Serialized field for knockback force
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D BoxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private float doubleJump;
    private float movementCooldown;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();
        collection = GameObject.Find("Collection Obj").GetComponent<CollManagement>();
    }

    private void Update()
    {
        if (movementCooldown > 0.35f)
        {
            if (body.linearVelocity.y > 0 && !isGrounded() && !onWall())
            {
                anim.SetTrigger("Jump");
            }
            else if (body.linearVelocity.y < 0 && !isGrounded() && !onWall()){
                anim.SetTrigger("Fall");
            }

            horizontalInput = Input.GetAxis("Horizontal");

            //Flip player when moving left-right
            if  (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            //Set animator parameters
            if (isGrounded()){
                anim.SetBool("Run", horizontalInput != 0);
            }
            anim.SetBool("Grounded", isGrounded());

            if (isGrounded() || onWall())
            {
                doubleJump = 1;
            }

            //Wall jump logic
            if (wallJumpCooldown > 0.2f)
            {
                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

                if (onWall() && !isGrounded() && collection.collItems >= 1)
                {
                    body.gravityScale = 1;
                    body.linearVelocity = Vector2.zero;
                    anim.SetBool("WallSlide", true);
                }
                else
                    body.gravityScale = 7;

                if (Input.GetButtonDown("Jump"))
                    Jump();
            }
            else
                wallJumpCooldown += Time.deltaTime;

            if(Input.GetButtonDown("Jump") && doubleJump > 0 && !isGrounded() && !onWall() && wallJumpCooldown > 0.2f)
            {
                DoubleJump();
            }
        }
        else
        {
            movementCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        else if (onWall() && !isGrounded() && collection.collItems >= 1)
        {
            if (horizontalInput == 0) 
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 6);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size*1.01f, 0, Vector2.down, 0.1f, groundLayer | cornerLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size*1.01f, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer | cornerLayer);
        if (collection.collItems >= 1)
        {
            return raycastHit.collider != null;
        }
        else
            return false;
    }

    private void DoubleJump()
    {
        if (collection.collItems >= 2)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            doubleJump--;
        }
    }

    public void knockback(int direction)
    {
        movementCooldown = 0;
        anim.SetBool("Run", false);
        body.linearVelocity = new Vector2(direction * knockbackForce, 10); // Use knockbackForce variable
    }
}
