using System.Collections;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public bool IsDestroyed = false;
    public float freezeDuration = 2f; // Thời gian đóng băng zombie (2 giây)
    public GameObject freezeEffectPrefab; // Prefab cho hiệu ứng đóng băng

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu zombie va chạm vào cây băng
        if (other.CompareTag("Zombie"))
        {
            // Đóng băng zombie
            ZombieMove zombie = other.GetComponent<ZombieMove>();
            if (zombie != null)
            {
                zombie.Freeze(freezeDuration);
                IsDestroyed = true;
                // Hiệu ứng đóng băng
                Instantiate(freezeEffectPrefab, other.transform.position, Quaternion.identity);
                
            }
        }
    }
}