using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (BuyMenuManager.Instance.BuyMenuIsOpen)
        {
            BuyMenuManager.Instance.OpenBuyMenu(false);
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.TogglePause(false);
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
