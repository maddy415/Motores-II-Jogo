using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform tra;
    public GameObject Bullet;
    public GameObject Enemy;
    
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashCooldown;
    public float timer;
    public bool canDash;
    public bool isJumping = false;
    private string direcao;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Shoot();
    }

    void Move()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
            direcao = "esq";

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
            direcao = "dir";
        }
        
        if((Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)))
        {
            anim.SetBool("Walk", false);
            Vector2 velocity = rb.velocity;
            rb.velocity = Vector2.SmoothDamp(new Vector2(rb.velocity.x, rb.velocity.y), Vector2.zero, ref velocity, 1f, Time.deltaTime);
        }
        //Dash
        if (!canDash)
        {
            timer += Time.deltaTime;
            if (timer > dashCooldown)
            {
                canDash = true;
                timer = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            if (direcao == "esq")
            {
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
                canDash = false;
            }
            else if (direcao == "dir")
            {
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                canDash = false;
            }
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
        
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }

    
    
}
