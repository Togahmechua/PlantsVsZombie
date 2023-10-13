using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieMove : MonoBehaviour
{
    private bool increase = false;
    private bool increase2 = false;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource eat;
    [SerializeField] private AudioSource leftEdgeAudio;
    private GameManager gameManager;
    public float currentHealth;
    private PLanting plants;
    public float speed;
    [SerializeField] private float currentSpeed;
    private Camera mainCamera;
    private float leftEdge;
    public float Range;
    public GameObject spawnPoint;
    public LayerMask layerMask;
    public int point;
    private bool isFrozen = false;
    private float freezeDuration = 0f;
    public float timeToMoveAgain = 0f;
    private bool hasCrossedLeftEdge = false;
    public float damage = 10f; // Số lượng máu sẽ bị trừ khi va chạm với plant.

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        speed = currentSpeed;
        plants = GetComponent<PLanting>();
        gameManager = FindObjectOfType<GameManager>();
        // Tính toán giới hạn bên phải của camera dựa trên tỷ lệ màn hình
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        leftEdge = mainCamera.transform.position.x - (cameraHeight * screenAspect * 0.5f) - 1f; // +1f để đảm bảo nó xóa đúng sau khi bay ra khỏi camera
    }

    // Update is called once per frame
   void Update()
{
    transform.position += Vector3.left * speed * Time.deltaTime;
    RaycastHit2D hit = Physics2D.Raycast(spawnPoint.transform.position, Vector2.left, Range, layerMask);
    
    // Kiểm tra nếu zombie đã vượt qua giới hạn và âm thanh chưa được phát
    if (transform.position.x < leftEdge && !hasCrossedLeftEdge)
    {
        gameManager.Canvas.enabled = true;
        Invoke(nameof(Change), 5f);

        // Kiểm tra nếu leftEdgeAudio không null
        if (leftEdgeAudio != null)
        {
            leftEdgeAudio.Play(); // Phát âm thanh khi zombie đi qua leftEdge
            hasCrossedLeftEdge = true; // Đặt biến này thành true để ngăn việc phát âm thanh nhiều lần
        }
    }
    
    // Kiểm tra va chạm với cây
    if (hit.collider != null)
    {
        this.speed = 0;
        // Kiểm tra va chạm với cây
        PLanting hitPlant = hit.collider.GetComponent<PLanting>();
        if (hitPlant != null)
        {
            eat.Play();
            // Trừ điểm máu của cây
            hitPlant.currentHealth -= damage * Time.deltaTime;
        }
    }
    else
    {
        this.speed = currentSpeed;
    }
    CheckFreeze();
    IcreaseSpeed();
}

    public void TakeDamage(float amount)
    {
        hit.Play();
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            gameManager.score += point;
        }
    }

    public void Freeze(float duration)
    {
        isFrozen = true;
        freezeDuration = duration;
    }

    private void CheckFreeze()
    {
        if (isFrozen == true)
        {
            this.speed = 0;
            timeToMoveAgain += Time.deltaTime;
        }
        if (timeToMoveAgain >= 2)
        {
            this.speed = currentSpeed;
            isFrozen = false;
            timeToMoveAgain = 0;
        }
    }

    private void IcreaseSpeed()
    {
        if (gameManager.score >= 1000 && increase == false)
        {
            currentSpeed = currentSpeed * 2;
            increase = true;
        }
        else if (gameManager.score >= 2000 && increase2 == false)
        {
            currentSpeed = currentSpeed * 2;
            increase2 = true;
        }
    }

    private void Change()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector2.left * Range);
    }
}
