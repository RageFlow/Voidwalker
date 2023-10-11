using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Buy_Station_Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static Buy_Station_Controller Instance;
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

    private string _interactText => $"(Shop) Press E";

    private bool _textVisible = true;

    public bool PlayerInRange => _playerInRange;
    private bool _playerInRange;

    private void FixedUpdate()
    {
        if (_playerInRange && !_textVisible)
        {
            SetTextActive(_interactText);
            _textVisible = true;
        }
        else if (!_playerInRange && _textVisible)
        {
            SetTextActive(string.Empty);
            _textVisible = false;
        }
    }

    private void SetTextActive(string value)
    {
        if (_text != null)
        {
            _text.SetText(value);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _playerInRange = false;
        }
    }
}
