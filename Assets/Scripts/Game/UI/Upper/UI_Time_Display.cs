using TMPro;
using UnityEngine;

public class UI_Time_Display : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (_text != null && DayNightManager.Instance != null)
        {
            _text.SetText($"Time\n{DayNightManager.Instance.GetgameTimeConverted():n0}");
        }
    }
}
