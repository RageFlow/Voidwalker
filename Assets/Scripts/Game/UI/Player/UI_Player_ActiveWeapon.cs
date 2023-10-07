using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_ActiveWeapon : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    private float _colorHue = 1f / 360f * 0f;
    private float _colorSaturation = 0f;
    private float _colorValue = 1f;

    void FixedUpdate()
    {
        if (GameManager.Instance != null)
        {
            _image.sprite = WeaponManager.Instance.ActiveWeapon.Sprite;

            _image.color = Color.HSVToRGB(_colorHue, _colorSaturation, _colorValue);
        }
    }
}
