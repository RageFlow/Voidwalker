using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UI_Menu_Controller : MonoBehaviour
{
    public static UI_Menu_Controller Instance;
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

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void IsVisible(bool value)
    {
        gameObject.SetActive(value);
    }
}
