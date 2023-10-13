using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePool : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject firebullet;
    private List<GameObject> bullets = new List<GameObject>();

    public GameObject GetBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newFireBullet = Instantiate(firebullet);
        bullets.Add(newFireBullet);
        return newFireBullet;
    }

    public void ReturnBulletBool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(parentTransform);
    }
}
