using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public List<Item_Values> Items => _items;
    private List<Item_Values> _items = new();

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

    public void AddItem(Item_Values item)
    {
        Item_Values currentItem = _items.FirstOrDefault(x => x.Name.Equals(item.Name));

        if (currentItem is Item_Values)
        {
            currentItem.AddAmount(item.Amount);
        }
        else
        {
            _items.Add(item);
        }
    }
}
