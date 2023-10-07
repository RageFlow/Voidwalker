using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ability_Controller : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;
    private Base_Ability_Class _abilityClass;

    void Start()
    {
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (_abilityClass != null && _abilityClass.TimeRemaining > 0f)
        {

        }
    }

    public void UpdateAbility(Base_Ability_Class abilityClass, Sprite sprite, string key)
    {
        _image.sprite = sprite;
        _text.text = key;
        _abilityClass = abilityClass;
    }
}
