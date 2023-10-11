using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Healthbar_Controller : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;
    
    void Start()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private float _colorHue = 1f / 360f * 110f;
    private float _colorSaturation = 1f;
    private float _colorValue = 0.6f;
    
    void FixedUpdate()
    {
        if (Player_Controller.Instance != null)
        {
            var healthRemaining = Player_Controller.Instance.GetHealthValue();
            _image.fillAmount = healthRemaining;

            float hue = _colorHue * healthRemaining;

            _image.color = Color.HSVToRGB(hue, _colorSaturation, _colorValue);

            _text.SetText($"{Player_Controller.Instance.PlayerHealth}/{Global_Values.PlayerMaxHealth}");
        }
    }
}
