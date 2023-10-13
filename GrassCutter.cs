using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrassCutter : MonoBehaviour
{
    [SerializeField] private AudioSource hit;
    private Camera mainCamera;
    private float rightEdge;
    public float speed;
    public GameObject spawnPoint;
    public LayerMask layerZombie;
    public float leight;
    private bool IsTouch = false;

     private void Start()
    {
     
        mainCamera = Camera.main;
        // Tính toán giới hạn bên phải của camera dựa trên tỷ lệ màn hình
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        rightEdge = mainCamera.transform.position.x + (cameraHeight * screenAspect * 0.5f) + 1f; // +1f để đảm bảo nó xóa đúng sau khi bay ra khỏi camera
    }

    // Update is called once per frame
    void Update()
    {
        Crash();
    }

    private void Crash()
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnPoint.transform.position, Vector2.right, leight, layerZombie);
        if (hit.collider != null)
        {
           IsTouch = true;
        }

        if (IsTouch == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (transform.position.x > rightEdge)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            hit.Play();
            Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(spawnPoint.transform.position, Vector2.right * leight);
    }
}
