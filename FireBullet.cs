using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed;
    private Camera mainCamera;
    private float rightEdge;
    private FirePool firePool;
    public float damage; // Lượng sát thương của viên đạn.


    private void Start()
    {
     
        mainCamera = Camera.main;
        firePool = FindObjectOfType<FirePool>();
        // Tính toán giới hạn bên phải của camera dựa trên tỷ lệ màn hình
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        rightEdge = mainCamera.transform.position.x + (cameraHeight * screenAspect * 0.5f) + 1f; // +1f để đảm bảo nó xóa đúng sau khi bay ra khỏi camera
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        // Kiểm tra xem viên đạn có ra khỏi bên phải của camera không
        if (transform.position.x > rightEdge)
        {
            firePool.ReturnBulletBool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Zombie"))
    {
        ZombieMove zombie = other.gameObject.GetComponent<ZombieMove>();
        if (zombie != null)
        {
            // Gây ra sát thương cho zombie
            zombie.TakeDamage(damage);
        }
        firePool.ReturnBulletBool(gameObject);
    }
}
}
