using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance == null)
        {
            Destroy(this);
        }
    }

    public void OnEscButton()
    {
        GameManager.Instance.TogglePause(!GameManager.Instance.GamePaused);
    }

    public void ResumeGame()
    {
        GameManager.Instance.TogglePause(false);
    }
}
