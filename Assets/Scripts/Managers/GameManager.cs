using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

        _gameActive = true;

        Time.timeScale = 1f;
    }

    // Global Features
    public bool GameActive => _gameActive;
    private bool _gameActive;
    
    public bool GamePaused => _gamePaused;
    private bool _gamePaused;

    public Texture2D GameCursorSprite => _gameCursorSprite;
    [SerializeField] private Texture2D _gameCursorSprite;


    public Transform ProjectileSpawnContainer => _projectileSpawnContainer;
    [SerializeField] private Transform _projectileSpawnContainer;

    public Transform ItemSpawnContainer => _itemSpawnContainer;
    [SerializeField] private Transform _itemSpawnContainer;

    public Transform MobSpawnContainer => _mobSpawnContainer;
    [SerializeField] private Transform _mobSpawnContainer;

    
    public float GlobalKillCount => _globalKillCount;
    private float _globalKillCount;
    public void AddKill(AI_Mob_Values mob)
    {
        if (mob != null)
        {
            _globalKillCount++;
        }
    }

    public void EndGame()
    {
        _gameActive = false;
        TogglePause(true);
    }

    private float GameEndCountdown = 2f;

    private void LateUpdate()
    {
        if (!_gameActive)
        {
            GameEndCountdown -= Time.deltaTime;
            DisableTimeScale();
        }
    }

    private void EnableTimeScale()
    {
        if (_gameActive)
        {
            Time.timeScale = 1f;
        }
    }
    private void DisableTimeScale()
    {
        if (_gameActive || !_gameActive && GameEndCountdown <= 0)
        {
            Time.timeScale = 0f;
        }
    }

    public void ToggleGamePause(bool value)
    {
        _gamePaused = value;
        if (value)
        {
            DisableTimeScale();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            EnableTimeScale();
            Cursor.SetCursor(_gameCursorSprite, Vector2.zero, CursorMode.Auto);
        }
    }

    public void TogglePause(bool value)
    {
        if (!_gameActive)
        {
            ToggleGamePause(true);
            UI_Menu_Controller.Instance.IsVisible(_gamePaused);
        }
        else
        {
            ToggleGamePause(value);
            UI_Menu_Controller.Instance.IsVisible(value);
        }
    }
}
