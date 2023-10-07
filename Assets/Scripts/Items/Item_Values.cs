using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Values : MonoBehaviour
{
    public string Name => _name;
    private string _name;

    public float Difficulty => _difficulty;
    private float _difficulty;

    public Sprite ItemSprite => _itemSprite;
    private Sprite _itemSprite;

    public float Value => _value;
    private float _value;

    public float Amount => _amount;
    private float _amount;

    public void AddAmount(float amount)
    {
        _amount += amount;
    }

    public void SetValues(Abstract_Item_Values values)
    {
        _name = values.Name;
        _difficulty = values.Difficulty;
        _itemSprite = values.ItemSprite;
        _value = values.Value;
        _amount = values.Amount;
    }
}
