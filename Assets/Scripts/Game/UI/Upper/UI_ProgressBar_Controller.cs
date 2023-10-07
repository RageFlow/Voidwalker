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
        if (GameManager.Instance != null)
        {
            var requirement = GameManager.Instance.GameLevelUpRequirement;

            var progress = GameManager.Instance.KillCount / requirement;

            _image.fillAmount = progress;

            _image.color = Color.HSVToRGB(_colorHue, _colorSaturation, _colorValue);

            _text.SetText($"{GameManager.Instance.KillCount}/{requirement}");
        }
    }
}
