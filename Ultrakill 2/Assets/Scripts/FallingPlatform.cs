using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private TargetJoint2D tj;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    
    public float fallingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        tj = GetComponent<TargetJoint2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Falling", fallingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "DestroyPlat")
        {
            Destroy(gameObject);
        }
    }

    private void Falling()
    {
        tj.enabled = false;
        bc.isTrigger = true;
    }

}
