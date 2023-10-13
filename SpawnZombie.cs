using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject zomebie;
    public Transform[] spawnSpots;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        timeBtwSpawns = startTimeBtwSpawns;
    }
    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            int randPos = Random.Range(0, spawnSpots.Length - 1);
            Instantiate(zomebie, spawnSpots[randPos].position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else 
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }  
}
