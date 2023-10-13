using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public Color green;
    public Color red;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isOccupied == true)
        {
            rend.color = red;
        }
        else
        {
            rend.color = green;
        }
    }

    public void UpdateOccupation(List<GameObject> deletedObjects)
    {
        // Kiểm tra nếu các cây đã được xóa chứa trong danh sách này
        foreach (GameObject obj in deletedObjects)
        {
            // Kiểm tra xem cây đã xóa có trên ô này không
            if (Vector3.Distance(obj.transform.position, transform.position) < 0.1f)
            {
                isOccupied = false;
                break;
            }
        }
    }
}