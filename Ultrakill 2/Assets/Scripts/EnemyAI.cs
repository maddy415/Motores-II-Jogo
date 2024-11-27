using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Vector3 PlayerPos;
    public float walkSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = player.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(PlayerPos.x, 0f, 0f) * walkSpeed;
    }
}
