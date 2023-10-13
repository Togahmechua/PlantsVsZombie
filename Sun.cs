using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private GameManager gameManager;
    private SunPool sunPool; // Reference to the SunPool

    public float speed;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sunPool = FindObjectOfType<SunPool>(); // Find the SunPool component
        if (gameManager == null)
        {
            Debug.LogError("Không tìm thấy GameManager trong scene.");
        }
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        CheckOutOfBounds();
    }

    private void OnMouseDown()
    {
        if (gameManager != null)
        {
            sunPool.ReturnSunToPool(gameObject);
            gameManager.gold += 50;
        }
        else
        {
            Debug.LogError("GameManager không được gán cho Sun.");
        }
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y <= -6.5f)
        {
            if (sunPool != null)
            {
                // Trả đối tượng mặt trời vào pool sau khi đi ra ngoài màn hình
                sunPool.ReturnSunToPool(gameObject);
            }
            else
            {
                Debug.LogError("SunPool không được gán cho Sun.");
            }
        }
    }
}
