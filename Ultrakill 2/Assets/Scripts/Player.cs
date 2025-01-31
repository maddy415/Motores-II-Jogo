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
    public GameObject Win;
    public GameObject Cenario;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
   
    
    
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
    public static bool vivo = true;
    private bool isWallsliding;
    public float wallSlidingspeed = 2f;
    private bool isWalljumping;
    private float wallJumpdirection;
    private float wallJumptime = 0.2f;
    private float wallJumpingcounter;
    private float wallJumpingduration = 0.4f;
    private Vector2 wallJumpingforce = new Vector2(8f, 16f);
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        Enemy = GameObject.FindGameObjectWithTag("Grunt");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Shoot();
        isWalled();
        WallSlide();
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Cenario.transform.Rotate(0, 0, 90);
        }
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
            vivo = false;
        }
        
        if (collision.gameObject.tag == "Finish")
        {
            isJumping = false;
            Win.SetActive(true);
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

        if (Input.GetMouseButtonDown(0) && canFire && vivo)
        {
            Instantiate(Bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            canFire = false;
            
            
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if(isWalled() && isJumping)
        {
            isWallsliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingspeed, float.MaxValue));
        }
        else
        {
            isWallsliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallsliding)
        {
            isWalljumping = false;
            wallJumpdirection = -transform.localScale.x;
            wallJumpingcounter = wallJumptime;
            
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingcounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingcounter > 0f)
        {
            isWalljumping = true;
            rb.velocity = new Vector2(wallJumpdirection * wallJumpingforce.x, wallJumpingforce.y);
            wallJumpingcounter = 0f;
        }

        if (transform.localScale.x != wallJumpdirection)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        Invoke(nameof(StopWallJumping), wallJumpingduration);
        
    }

    private void StopWallJumping()
    {
        isWalljumping = false;
    }
    
}
