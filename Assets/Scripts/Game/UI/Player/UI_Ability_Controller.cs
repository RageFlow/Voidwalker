using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ability_Controller : MonoBehaviour
{
    private Image _image;
    private Image _overlay;
    private TextMeshProUGUI _text;
    private Base_Ability_Class _abilityClass;

    private void Awake()
    {
        var images = GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            if (image.gameObject.name == "Image")
            {
                _image = image;
            }
            else if (image.gameObject.name == "Overlay")
            {
                _overlay = image;

                _overlay.fillAmount = 0f;
            }
        }

        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (_abilityClass != null)
        {
            if (_abilityClass.TimeRemaining > 0f)
            {
                _overlay.fillAmount = _abilityClass.GetCooldownFloat();
            }
            else
            {
                _overlay.fillAmount = 0f;
            }
        }
    }

    public void UpdateAbility(Base_Ability_Class abilityClass, Sprite sprite, string key)
    {
        _image.sprite = sprite;
        _text.text = key;
        _abilityClass = abilityClass;
    }
}
