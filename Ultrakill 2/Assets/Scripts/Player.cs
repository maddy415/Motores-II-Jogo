using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform tra;
    private SpriteRenderer sr;
    public GameObject Bullet;
    public GameObject Enemy;
    public GameObject GameOver;
    
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashCooldown;
    public float timer;
    public bool canDash;
    public bool isJumping = false;
    private string direcao;
    public bool canFire = true;
    public float fireTimer;
    public float fireCooldown;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
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
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }

        if (collision.gameObject.tag == "Grunt")
        {
            sr.enabled = false;
            GameOver.SetActive(true);
            //Enemy.SetActive(false);
        }
    }

    void Shoot()
    {
        if (!canFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > fireCooldown)
            {
                canFire = true;
                fireTimer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Instantiate(Bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            canFire = false;
            
            
        }
    }

    
    
}
