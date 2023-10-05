using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Instance;

    public float PlayerHealth => _playerHealth;
    private float _playerHealth = 100;
    public float PlayerMaxHealth => _playerMaxHealth;
    private float _playerMaxHealth = 100;

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

    public float GetHealthValue()
    {
        return _playerHealth / _playerMaxHealth;
    }

    public void UpdateHealth(float value)
    {
        _playerHealth += value;
    }
}
