using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public List<Item_Values> Items => _items;
    private List<Item_Values> _items = new();

    public UnityEvent OnUpdated;

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

    public void UpdatePlayerValues()
    {
        Player_Controller.Instance.UpdatePickupRadius();
    }

    public float GetTotalMoney()
    {
        var allMoney = _items.Select(x => Mathf.Round(x.Amount * x.Value)).Sum();

        return allMoney;
    }

    public bool CanBuyFor(float value)
    {
        return GetTotalMoney() >= value;
    }

    public void UpdateMoney(float amount)
    {
        float extraAmount = amount;

        foreach (var item in _items)
        {
            if (extraAmount <= 0f)
            {
                continue;
            }

            var totalToRemove = extraAmount / item.Value;

            var itemRemaining = item.RemoveAmount(totalToRemove);

            extraAmount = itemRemaining * item.Value;
        }

        if (extraAmount > 0f || extraAmount < 0f)
        {
            Debug.LogWarning($"Unable to convert price to items {extraAmount} is remaining!");
        }

        OnUpdated.Invoke();
    }

    public void UpdateItem(string name, float amount)
    {
        Item_Values currentItem = _items.FirstOrDefault(x => x.Name.Equals(name));

        if (currentItem != null)
        {
            currentItem.AddAmount(amount);
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

        if (item is Item_Values)
        {
            StageManager.Instance.AddPoint(Mathf.Floor(item.Amount * item.Value));
        }
    }
}
