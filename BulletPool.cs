using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject bulletPrefab;
    private List<GameObject> bullets = new List<GameObject>();

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab);
        bullets.Add(newBullet);
        return newBullet;
    }


    public void ReturnBulletBool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(parentTransform);
    }
}
