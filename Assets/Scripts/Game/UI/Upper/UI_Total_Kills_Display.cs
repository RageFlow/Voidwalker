using TMPro;
using UnityEngine;

public class UI_Total_Kills_Display : MonoBehaviour
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
            _text.SetText($"Kills\n{GameManager.Instance.GlobalKillCount:n0}");
        }
    }
}
