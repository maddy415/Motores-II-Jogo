using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    
    public float walkSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() 
    {
        if (transform.position.x > playerTransform.transform.position.x)
        {
            transform.position += Vector3.left * walkSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
            
        }
        
        if (transform.position.x < playerTransform.transform.position.x)
        {
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
            
        }
        
    }
}
