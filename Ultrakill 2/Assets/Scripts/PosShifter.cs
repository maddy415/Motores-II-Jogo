using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosShifter : MonoBehaviour
{
    public float minimumTime;
    public float maximumTime;
    private float timeUntil;
    private float timerBack;
    public Transform spawnTransformleft;
    public Transform spawnTransformright;

    public bool podeVoltar;
    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntil -= Time.deltaTime;
        if (timeUntil <= 0 && !podeVoltar)
        {
            transform.position = new Vector2(spawnTransformleft.position.x, spawnTransformleft.position.y);
            SetTimeUntilSpawn();
            podeVoltar = true;
        }
        
        timerBack -= Time.deltaTime;
        if (timerBack <= 0 && podeVoltar)
        {
            transform.position = new Vector2(spawnTransformright.position.x, spawnTransformright.position.y);
            podeVoltar = false;
            SetTimeUntilSpawn();
        }
        
    }

    private void SetTimeUntilSpawn()
    {
        timeUntil = Random.Range(minimumTime, maximumTime);
        timerBack = Random.Range(minimumTime, maximumTime);
    }
}