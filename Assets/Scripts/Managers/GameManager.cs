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
    }

    private float _gameDifficulty = 1f; // How difficult
    private float _gameLevelUpRequirement => _gameDifficulty * 40 * _gameStage * _gameStage;

    public float GameStage => _gameStage;
    private float _gameStage = 1f;

    public Transform ProjectileSpawnContainer => _projectileSpawnContainer;
    [SerializeField] private Transform _projectileSpawnContainer;

    public float KillCount;
    private float _killCount;

    public void AddKill(AI_Mob_Values mob)
    {
        if (mob != null)
        {
            _killCount++;
        }

        if (_killCount >= _gameLevelUpRequirement)
        {
            _gameStage++;
        }
    }
}
