using System.Collections.Generic;
using UnityEngine;

public class SunPool : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject sunPrefab; // Đối tượng mặt trời mẫu để tạo ra từ pool
    private List<GameObject> suns = new List<GameObject>(); // Danh sách các đối tượng mặt trời trong pool

    // Hàm này được gọi khi bạn muốn tạo ra một đối tượng mặt trời từ pool
    public GameObject GetSun()
    {
        // Tìm đối tượng mặt trời không hoạt động trong pool để tái sử dụng
        foreach (GameObject sun in suns)
        {
            if (!sun.activeInHierarchy)
            {
                sun.SetActive(true);
                return sun;
            }
        }

        // Nếu không tìm thấy đối tượng mặt trời không hoạt động, tạo mới một đối tượng mặt trời
        GameObject newSun = Instantiate(sunPrefab);
        suns.Add(newSun);
        return newSun;
    }

    // Hàm này được gọi khi bạn muốn trả đối tượng mặt trời vào pool
    public void ReturnSunToPool(GameObject sun)
    {
        sun.SetActive(false);
        sun.transform.SetParent(parentTransform);
    }
}
