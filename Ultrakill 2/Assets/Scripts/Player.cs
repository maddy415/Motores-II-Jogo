using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float jumpForce;
    public float dashForce;
    public bool isJumping = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        if((Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)))
        {
            anim.SetBool("Walk", false);
            Debug.Log("soltou");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(new Vector2(dashForce, 0f), ForceMode2D.Force);
        }
    }


    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (isJumping!=true))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("Jump", true);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
    }
}
