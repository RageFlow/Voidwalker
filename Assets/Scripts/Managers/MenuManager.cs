using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public List<Abstract_UI_Option_Values> OptionValues => _optionValues;
    [SerializeField] private List<Abstract_UI_Option_Values> _optionValues;

    [SerializeField] private UI_Options_Menu_Controller _optionsMenuController;

    [SerializeField] private GameObject _gameMenuObject;

    public void OptionsChanged()
    {
        _optionsMenuController.SetChanged();
    }

    public void OnEscButton()
    {
        ToggleGameMenu();
    }

    public void ToggleGameMenu()
    {
        if (GameManager.Instance.GameActive) // If game is still Active
        {
            if (BuyMenuManager.Instance.BuyMenuIsOpen) // If Buy menu is open
            {
                BuyMenuManager.Instance.IsBuyMenuVisible(false); // Close BuyMenu and Unpause Game
            }
            else if (GameManager.Instance.GamePaused) // False and Game is Paused
            {
                if (UI_Menu_Controller.Instance.IsMenuVisible && UI_Options_Menu_Controller.Instance.IsOptionsVisible) // If Options menu is oen
                {
                    OpenOptionsMenu(false); // Close Options Menu
                    _gameMenuObject.SetActive(true);
                    UI_Menu_Controller.Instance.OptionTextVisible(false);
                }
                else if (UI_Menu_Controller.Instance.IsMenuVisible)
                {
                    ResumeGame(); // Resume game
                }
            }
            else
            {
                UI_Menu_Controller.Instance.IsVisible(true);
                GameManager.Instance.ToggleGamePause(true);
            }
        }
        else // If game has ended
        {
            _gameMenuObject.SetActive(true);
            UI_Options_Menu_Controller.Instance.IsVisible(false);
            UI_Menu_Controller.Instance.OptionTextVisible(false);

            GameManager.Instance.ToggleGamePause(true);
            UI_Menu_Controller.Instance.IsVisible(true);
            BuyMenuManager.Instance.IsBuyMenuVisible(false);
        }
    }

    public void OpenOptionsMenu(bool value)
    {
        _gameMenuObject.SetActive(false);
        UI_Options_Menu_Controller.Instance.IsVisible(value);
        UI_Menu_Controller.Instance.OptionTextVisible(value);
    }

    public void ResumeGame()
    {
        GameManager.Instance.ToggleGamePause(false);
        UI_Menu_Controller.Instance.IsVisible(false);
        BuyMenuManager.Instance.IsBuyMenuVisible(false);
        UI_Options_Menu_Controller.Instance.IsVisible(false);
        GameManager.Instance.ToggleGamePause(false);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
