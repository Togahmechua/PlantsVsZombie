using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ban : MonoBehaviour
{
    private Ice ice;
    public GameObject ban;
    void Start()
    {
        ice = GetComponent<Ice>();
    }

        void Update()
    {
        CheckDie();
    }

    private void CheckDie()
    {
        if (ice.IsDestroyed == true)
        {
            Instantiate(ban, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
