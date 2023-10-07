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

    private CircleCollider2D _pickupCollider;

    public float PickupRadius => _pickupRadius;
    private float _pickupRadius = 1f;

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

        if (_pickupCollider == null)
        {
            var colliders = GetComponents<CircleCollider2D>();

            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                {
                    _pickupCollider = collider;
                }
            }
        }
    }

    public float GetHealthValue()
    {
        return _playerHealth / _playerMaxHealth;
    }

    public void UpdateHealth(float value)
    {
        _playerHealth += value;

        if (_playerHealth >= 0f)
        {
            GameManager.Instance.EndGame();
        }
    }
    
    public void UpdatePickupRadius(float value)
    {
        if (_pickupCollider != null)
        {
            _pickupRadius = value;
            _pickupCollider.radius = _pickupRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item")
        {
            var component = collider.gameObject.GetComponent<Item_Values>();

            if (component != null)
            {
                PlayerManager.Instance.AddItem(component);
                Destroy(collider.gameObject);
            }
        }
    }
}
