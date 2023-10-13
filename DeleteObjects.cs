using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteObjects : MonoBehaviour
{
    public GameObject myPrefab; // Kéo và thả prefab vào đây
    public Button deleteButton; // Kéo và thả nút vào đây
    public bool isDestroy = false;

    // Định nghĩa sự kiện để thông báo rằng đã xóa các đối tượng
    public delegate void ObjectsDeleted(List<GameObject> deletedObjects);
    public static event ObjectsDeleted OnObjectsDeleted;

    private void Start()
    {
        deleteButton.onClick.AddListener(DeleteAllPrefabs);
    }

    public void DeleteAllPrefabs()
    {
        List<GameObject> deletedObjects = new List<GameObject>();

        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag(myPrefab.tag);

        foreach (GameObject prefab in allPrefabs)
        {
            ToDestroy toDestroyComponent = prefab.GetComponentInChildren<ToDestroy>();

            if (toDestroyComponent != null && toDestroyComponent.isSelected)
            {
                deletedObjects.Add(prefab);
                Destroy(prefab);
                isDestroy = true;
            }
        }

        // Gửi thông báo qua sự kiện rằng đã xóa các đối tượng
        if (OnObjectsDeleted != null)
        {
            OnObjectsDeleted(deletedObjects);
        }
    }
}