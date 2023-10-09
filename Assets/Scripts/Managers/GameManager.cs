using UnityEngine;

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
    }

    // Global Features
    public bool GameActive => _gameActive;
    private bool _gameActive;
    
    public bool GamePaused => _gamePaused;
    private bool _gamePaused;

    public Texture2D GameCursorSprite => _gameCursorSprite;
    [SerializeField] private Texture2D _gameCursorSprite;

    // Game specific
    private float _gameDifficulty = 1f; // How difficult

    public float GameLevelUpRequirement => _gameLevelUpRequirement;
    private float _gameLevelUpRequirement => _gameDifficulty * 40 * _gameStage * _gameStage;

    public float GameStage => _gameStage;
    private float _gameStage = 1f;
    
    public float GameStagesTotal => _gameStagesTotal;
    private float _gameStagesTotal = 99f;

    public Transform ProjectileSpawnContainer => _projectileSpawnContainer;
    [SerializeField] private Transform _projectileSpawnContainer;

    public Transform ItemSpawnContainer => _itemSpawnContainer;
    [SerializeField] private Transform _itemSpawnContainer;

    public Transform MobSpawnContainer => _mobSpawnContainer;
    [SerializeField] private Transform _mobSpawnContainer;

    public float PointCount => _pointCount;
    private float _pointCount;
    public float GlobalPointCount => _globalPointCount;
    private float _globalPointCount;

    
    public float GlobalKillCount => _globalKillCount;
    private float _globalKillCount;
    public void AddKill(AI_Mob_Values mob)
    {
        if (mob != null)
        {
            _globalKillCount++;
        }
    }

    public void AddPoint(float value)
    {
        _pointCount += value;
        _globalPointCount += value;

        if (_pointCount >= _gameLevelUpRequirement)
        {
            var extraPoints = _pointCount - _gameLevelUpRequirement;
            _gameStage++;
            _pointCount = extraPoints;
        }
    }

    public void EndGame()
    {
        _gameActive = false;
    }
    public void TogglePause(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.SetCursor(_gameCursorSprite, Vector2.zero, CursorMode.Auto);
        }
        _gamePaused = value;
        UI_Menu_Controller.Instance.IsVisible(_gamePaused);
    }
}
