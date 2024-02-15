using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProgressBar_Controller : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;

    void Start()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private float _colorHue = 1f / 360f * 220f; // Light Blue
    private float _colorSaturation = 1f;
    private float _colorValue = 0.6f;

    void FixedUpdate()
    {
        if (_text != null && _image != null && GameManager.Instance != null)
        {
            var requirement = StageManager.Instance.GameLevelUpRequirement;

            var progress = StageManager.Instance.PointCount / requirement;

            _image.fillAmount = progress;

            _image.color = Color.HSVToRGB(_colorHue, _colorSaturation, _colorValue);

            _text.SetText($"{StageManager.Instance.PointCount}/{requirement}");
        }
    }
}
