using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherries : MonoBehaviour
{
    [SerializeField] private AudioSource explo;
    public bool IsDestroyeds = false;
    public float Range;
    public float damage;
    public GameObject explosionPrefab; // Prefab của hiệu ứng nổ
    public LayerMask enemyLayer;
    public GameObject ban;
    public float explosionDelay; // Thời gian trước khi cherries nổ

    private void Start()
    {
        // Khởi động hàm nổ sau một khoảng thời gian
        StartCoroutine(ExplodeAfterDelay());
    }
    private void Update()
    {
        Invoke(nameof(DealDmg), explosionDelay);
    }

    private IEnumerator ExplodeAfterDelay()
    {
        explo.Play();
        yield return new WaitForSeconds(explosionDelay);
        
        // Hiệu ứng nổ
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Instantiate(ban, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void DealDmg()
    {
        Collider2D[] findeds = Physics2D.OverlapCircleAll(transform.position, Range, enemyLayer);
        if (findeds == null || findeds.Length <= 0) return;

        for (int i = 0; i < findeds.Length; i++)
        {
            var finded = findeds[i];
            if (!finded) continue; // Sử dụng !finded để kiểm tra collider có tồn tại

            var zomebie = finded.GetComponent<ZombieMove>();
            if (!zomebie) continue;
            zomebie.TakeDamage(damage);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
