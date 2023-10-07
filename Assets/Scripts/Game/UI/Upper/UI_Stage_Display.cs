using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stage_Display : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null)
        {
            _text.SetText($"Stage\n{GameManager.Instance.GameStage}/{GameManager.Instance.GameStagesTotal}");
        }
    }
}
