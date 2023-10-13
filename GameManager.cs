using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgrms;
    public Behaviour Canvas;
    public int gold;
    public int score;
    public Text goldDisplay;
    public Text ScoreDisplay;
    [SerializeField] private PLanting pLantingToPlace;
    public GameObject grid;
    public CustomCursor customCursor;
    public Tile[] tiles;
    public float timetospawnSun;
    private float timeSinceLastSpawn;
    public GameObject Sun;
    private float MaxX = 20f;
    private float MinX = -8f;
    private List<GameObject> spawnedSuns = new List<GameObject>(); // Danh sách các mặt trời đã tạo
    private bool isTimePaused = false;
   private void Start()
{
    timeSinceLastSpawn = timetospawnSun - 3;
    DeleteObjects.OnObjectsDeleted += HandleObjectsDeleted;
}

private void HandleObjectsDeleted(List<GameObject> deletedObjects)
{
    // Duyệt qua danh sách các ô và cập nhật isOccupied dựa trên các cây đã xóa
    foreach (Tile tile in tiles)
    {
        tile.UpdateOccupation(deletedObjects);
    }
}

    private void Update()
    {
        ShowPoint();
        goldDisplay.text = "SUN : " + gold.ToString();
        ScoreDisplay.text = "Score : " + score.ToString(); 
        if (Input.GetMouseButtonDown(0) && pLantingToPlace != null)
        {
            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;
            foreach (Tile tile in tiles)
            {
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearestTile = tile;
                }
            }
            if (nearestTile.isOccupied == false)
            {
                Instantiate(pLantingToPlace, nearestTile.transform.position, Quaternion.identity);
                pLantingToPlace = null;
                nearestTile.isOccupied = true;
                grid.SetActive(false);
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }

        timeSinceLastSpawn += Time.deltaTime;
        SpawnSun();
    }


    public void BuyPlants(PLanting pLanting)
    {
        if (gold >= pLanting.cost)
        {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = pLanting.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;

            gold -= pLanting.cost;
            pLantingToPlace = pLanting;
            grid.SetActive(true);
        }
    }

    private void SpawnSun()
    {
        if (timeSinceLastSpawn >= timetospawnSun)
        {
            float randomX = Random.Range(MinX, MaxX);
            GameObject newSun = Instantiate(Sun, new Vector3(randomX, 8.88f, 0f), Quaternion.identity);
            spawnedSuns.Add(newSun); // Thêm mặt trời mới vào danh sách

            timeSinceLastSpawn = 0;
        }
    }

    private void ShowPoint()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Canvas.enabled = !Canvas.enabled;
            ToggleTimeScale();
        }
    }

    private void ToggleTimeScale()
    {
        if (isTimePaused)
        {
            Time.timeScale = 0f; 
        }
        else
        {
            Time.timeScale = 1f;
        }
    
        isTimePaused = !isTimePaused; // Chuyển đổi trạng thái
    }
}
