using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    public float speed;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        
    }

    void Jump()
    {
        
    }
}
