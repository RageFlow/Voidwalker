using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu_Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _menuText;

    [SerializeField] private GameObject _resumeButton;

    [SerializeField] private GameObject _restartButton;

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
