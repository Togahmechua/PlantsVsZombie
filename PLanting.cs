using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLanting : MonoBehaviour
{
    public int cost;
    private SpriteRenderer spr;
    public Sprite newspr;
    
    public GameObject ban;
    public float currentHealth;
    [SerializeField] private float MaxHealth;

    private Tile occupiedTile; // Thêm trường này để theo dõi Tile mà Planting chiếm giữ

    private void Start()
    {
        currentHealth = MaxHealth;
        spr = GetComponent<SpriteRenderer>();
        occupiedTile = GetComponent<Tile>();
    }

    private void Update()
    {
        Dead();
    }

    private void Dead()
    {
        if (currentHealth <= 0)
        {
            // Kiểm tra xem có Tile nào được chiếm giữ và đặt isOccupied của nó thành false
            if (occupiedTile != null)
            {
                occupiedTile.isOccupied = false;
                Debug.Log("a");
            }

            Destroy(gameObject);
            Instantiate(ban, transform.position, Quaternion.identity);
        }
        else if (currentHealth <= MaxHealth / 2)
        {
            spr.sprite = newspr;
        }
    }

    // Thêm phương thức để gán Tile cho đối tượng Planting
    public void SetOccupiedTile(Tile tile)
    {
        occupiedTile = tile;
    }
}
