using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu_Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _menuText;

    [SerializeField] private GameObject _resumeButton;

    [SerializeField] private GameObject _restartButton;

    public bool IsMenuVisible;

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
        IsMenuVisible = value;
        if (_menuText != null && _resumeButton != null && _restartButton != null)
        {
            if (!GameManager.Instance.GameActive)
            {
                gameObject.SetActive(true);
                _menuText.SetText("GAME OVER!");
                _resumeButton.SetActive(false);
                _restartButton.SetActive(true);
            }
            else
            {
                gameObject.SetActive(value);
                _resumeButton.SetActive(true);
                _restartButton.SetActive(false);
            }
        }
    }

    public void OptionTextVisible(bool value)
    {
        if (value)
        {
            _menuText.SetText("OPTIONS");
        }
        else
        {
            _menuText.SetText("GAME PAUSED");
        }
    }
}
