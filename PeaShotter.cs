using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShotter : MonoBehaviour
{
    [SerializeField] private AudioSource shot;
    public GameObject bullet;
    public GameObject spawnPoint;
    public LayerMask targetLayer;
    public float cooldownTime; // Thời gian cooldown giữa các lần bắn (ví dụ: 2 giây)

    private float timeSinceLastShot;
    public float leight;
    private BulletPool bulletPool;
    private FirePool firePool;

    private void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
        firePool = FindObjectOfType<FirePool>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnPoint.transform.position, Vector2.right, Mathf.Infinity, targetLayer);
        timeSinceLastShot += Time.deltaTime; // Tính toán thời gian kể từ lần bắn trước đó

        if (timeSinceLastShot >= cooldownTime && hit.collider != null) // Kiểm tra xem người chơi đã bắn và đã hết cooldown chưa
        {
            shot.Play();
            Shoot();
            timeSinceLastShot = 0f; // Đặt lại thời gian bắn
        }
    }

    private void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();
        
        if (bullet != null)
        {
            bullet.transform.position = spawnPoint.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(spawnPoint.transform.position, Vector2.right * leight);
    }
}