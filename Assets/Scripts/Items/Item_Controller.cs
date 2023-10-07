using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    private Item_Values _item_Values;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _item_Values = GetComponent<Item_Values>();

        if (_item_Values != null)
        {
            _spriteRenderer.sprite = _item_Values.ItemSprite;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Item values was not found!");
        }
    }
}
