using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public static MobSpawner Instance;

    public GameObject _mobPrefab;

    public float _defaultSpawnTime;
    public float _defaultAmount;
    public float _spawnRadius;

    public List<Vector2> _spawnLocations;

    private float _spawnTime;
    private float _spawnTimer;

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

        _spawnTimer = _defaultSpawnTime;
    }

    private void FixedUpdate()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f && GameManager.Instance.GameActive)
        {
            SpawnMobs();
            UdpateSpawnTimer();
        }
    }

    void UdpateSpawnTimer()
    {
        // Changing spawn time
        //_spawnTime = Mathf.Round(_defaultSpawnTime / GameManager.Instance.GameStage) + 1f;
        _spawnTime = _defaultSpawnTime;

        _spawnTimer = _spawnTime;
    }

    void SpawnMobs()
    {
        List<Vector2> possibleSpawns = _spawnLocations.Where(x =>
            x.x + _spawnRadius < Player_Movement.Instance.PlayerPosition.x ||
            x.x - _spawnRadius > Player_Movement.Instance.PlayerPosition.x ||
            x.y + _spawnRadius < Player_Movement.Instance.PlayerPosition.y ||
            x.y - _spawnRadius > Player_Movement.Instance.PlayerPosition.y
        ).ToList();

        List<Abstract_Mob_Values> Mobs = MobManager.Instance.Mobs.Where(x => x.Difficulty <= GameManager.Instance.GameStage).ToList();

        int spawnAmount = (int)_defaultAmount;

        if (GameManager.Instance.GameStage > 1)
        {
            spawnAmount = (int)Mathf.Round(_defaultAmount * GameManager.Instance.GameStage / 2);
        }

        foreach (var mob in Mobs)
        {
            for (int i = 0; i < Mathf.Round(Random.Range(Mathf.Round(spawnAmount / 2) + 1, spawnAmount) / (mob.Difficulty * 2)) + 1; i++)
            {
                int index = Random.Range(0, possibleSpawns.Count);

                Vector2 specificLocation = possibleSpawns[index];

                Vector3 location = new Vector3(Random.Range(-2, 4) + specificLocation.x, Random.Range(-2, 4) + +specificLocation.y, 0f);
                Quaternion rotation = new Quaternion();

                GameObject newMob = Instantiate(_mobPrefab, location, rotation, GameManager.Instance.MobSpawnContainer);

                AI_Mob_Values mobValues = newMob.GetComponent<AI_Mob_Values>();

                mobValues.SetValues(mob);
            }
        }
    }
}
