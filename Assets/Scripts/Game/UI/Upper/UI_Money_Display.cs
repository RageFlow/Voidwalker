using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Money_Display : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (_text != null && PlayerManager.Instance != null)
        {
            _text.SetText($"Money: {PlayerManager.Instance.GetTotalMoney():n0}");
        }
    }
}
