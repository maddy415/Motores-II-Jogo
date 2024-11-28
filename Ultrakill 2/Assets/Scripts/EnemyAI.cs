using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float walkSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.position += Vector3.left * walkSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
            Debug.Log("x maior");
        }
        
        if (transform.position.x < player.transform.position.x)
        {
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
            Debug.Log("x menor");
        }
        
        
        
        /*Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale; */
    }
}
