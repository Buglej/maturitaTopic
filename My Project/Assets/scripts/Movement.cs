using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float speed;
    public float jumpPower;
    private Animator anim;
    private bool Grounded;
    // Start is called before the first frame update
    void Start()
    {
        // Reference 
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // Promìná pro pohyb doleva/doprava
        float horizontalInput = Input.GetAxis("Horizontal");

        // Pohyb do stran
        myRigidbody.velocity = new Vector2(horizontalInput * speed, myRigidbody.velocity.y);

        // Otoèení charakteru po smìru pohybu
        if (horizontalInput > 0.01)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Skok
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Animace
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", Grounded);
    }


    private void Jump()
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpPower);
        Grounded = false;
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Grounded = true;
    }
}
