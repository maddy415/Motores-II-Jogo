using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;

    public float minimumTime;
    public float maximumTime;
    private float timeUntil;
    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntil -= Time.deltaTime;
        if (timeUntil <= 0)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntil = Random.Range(minimumTime, maximumTime);
    }
}
