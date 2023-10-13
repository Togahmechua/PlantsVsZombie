using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : MonoBehaviour
{
    public float cooldownTime; // Thời gian cooldown giữa các lần bắn (ví dụ: 2 giây)
    private float timeSinceLastSpawn;
    private SunPool sunPool;

    private void Start()
    {
        timeSinceLastSpawn = cooldownTime - 2;
        sunPool = FindObjectOfType<SunPool>();
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= cooldownTime)
        {
            GameObject sun = sunPool.GetSun(); // Get a sun object from the pool
            if (sun != null)
            {
                // Set other properties of the sun object if needed
                sun.transform.position = transform.position;
            }
            timeSinceLastSpawn = 0;
        }
    }
}
