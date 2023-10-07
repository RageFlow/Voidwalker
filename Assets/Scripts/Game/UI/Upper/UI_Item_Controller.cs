using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item_Controller : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;

    public string Name => _name;
    private string _name;
    
    public float Amount => _amount;
    private float _amount;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateAmount(float value)
    {
        _amount = value;
        _text.SetText(_amount.ToString());
    }

    public void SetInfo(Item_Values values)
    {
        _image.sprite = values.ItemSprite;
        _name = values.Name;
        _amount = values.Amount;
        _text.SetText(_amount.ToString());
    }

}
