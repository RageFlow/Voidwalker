using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Items_Collected_Controller : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;

    private List<UI_Item_Controller> _ui_items = new List<UI_Item_Controller>();

    private void Awake()
    {
        gameObject.RemoveChildren();
    }

    private void Start()
    {
        PlayerManager.Instance.OnUpdated.AddListener(UpdateUI);
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnUpdated.RemoveListener(UpdateUI);
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_itemPrefab != null && ItemManager.Instance != null)
        {
            foreach (var item in PlayerManager.Instance.Items)
            {
                var uiItem = _ui_items.FirstOrDefault(x => x.Name.Equals(item.Name));

                if (uiItem != null)
                {
                    uiItem.UpdateAmount(Mathf.Round(item.Amount));
                }
                else
                {
                    GameObject newItem = Instantiate(_itemPrefab, transform);

                    UI_Item_Controller itemController = newItem.GetComponent<UI_Item_Controller>();

                    if (itemController != null)
                    {
                        itemController.SetInfo(item);
                        _ui_items.Add(itemController);
                    }
                }
            }
        }
    }
}
