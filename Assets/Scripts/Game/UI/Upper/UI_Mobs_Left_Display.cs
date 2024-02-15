using TMPro;
using UnityEngine;

public class UI_Mobs_Left_Display : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (_text != null && MobManager.Instance != null)
        {
            _text.SetText($"Alive\n{MobManager.Instance.MobCountMax - MobManager.Instance.SpawnableMobsLeft:n0}/{MobManager.Instance.MobCountMax:n0}");
        }
    }
}
