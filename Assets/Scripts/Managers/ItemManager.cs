using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public GameObject DroppedItemPrefab => _droppedItemPrefab;
    [SerializeField] private GameObject _droppedItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance == null)
        {
            Destroy(this);
        }
    }

    public void SpawnItem(Vector3 position, Abstract_Item_Values item)
    {
        GameObject newItem = Instantiate(_droppedItemPrefab, position, new Quaternion(), GameManager.Instance.ItemSpawnContainer);

        Item_Values itemValues = newItem.GetComponent<Item_Values>();

        itemValues.SetValues(item);
    }
}
