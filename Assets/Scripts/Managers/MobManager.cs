using System;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public static MobManager Instance;

    public List<Abstract_Mob_Values> Mobs => _mobs;
    [SerializeField] private List<Abstract_Mob_Values> _mobs;

    public int MobBaseCountMax;

    public int MobCountMax => _currentMobCountMax;
    private int _currentMobCountMax = 0;

    public bool CanSpawnMobs => MobCount <= MobCountMax;
    public int SpawnableMobsLeft => MobCountMax - MobCount;

    public int MobCount => _activeMobs.Count;
    public List<string> ActiveMobs => _activeMobs;
    private List<string> _activeMobs = new();

    public List<Abstract_Item_Values> Items => _items;
    [SerializeField] private List<Abstract_Item_Values> _items;

    public void UpdateMaxMobCount()
    {
        _currentMobCountMax = MobBaseCountMax * (int)Math.Round(StageManager.Instance.GameStage);
    }

    public void AddMobToMoblist(string mobId)
    {
        if (_activeMobs != null)
        {
            _activeMobs.Add(mobId);
        }
    }
    public void RemoveMobFromMoblist(string mobId)
    {
        if (_activeMobs != null)
        {
            _activeMobs.Remove(mobId);
        }
    }

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
        _currentMobCountMax = MobBaseCountMax;
    }
}
