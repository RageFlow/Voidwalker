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

    public float RemoveAmount(float value)
    {
        _amount -= value;
        if (_amount < 0f)
        {
            var remaining = _amount * -1f;
            _amount = 0f;
            return remaining;
        }
        else
        {
            return 0f;
        }
    }

    public void SetValues(Abstract_Item_Values values)
    {
        _name = values.Name;
        _difficulty = values.Difficulty;
        _amount = Mathf.Floor((values.Amount * StageManager.Instance.GameStage) / values.Difficulty);

        if (_amount > 1 && _amount <= 3)
        {
            _itemSprite = values.ItemTripleSprite;
        }
        else if (_amount > 3)
        {
            _itemSprite = values.ItemMultiSprite;
        }
        else
        {
            _itemSprite = values.ItemSprite;
        }
        _value = values.Value;
    }
}
