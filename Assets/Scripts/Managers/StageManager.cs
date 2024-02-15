using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
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

    // Game specific

    public float GameLevelUpRequirement => _gameLevelUpRequirement;
    private float _gameLevelUpRequirement => Global_Values.GameDifficulty * 40 * _gameStage * _gameStage;

    public float GameStage => _gameStage;
    private float _gameStage = 1f;

    public float GameStagesTotal => _gameStagesTotal;
    private float _gameStagesTotal = 99f;

    public float PointCount => _pointCount;
    private float _pointCount;
    public float GlobalPointCount => _globalPointCount;
    private float _globalPointCount;

    public void CheckStageUpdate()
    {
        if (_pointCount >= _gameLevelUpRequirement)
        {
            var extraPoints = _pointCount - _gameLevelUpRequirement;
            _gameStage++;
            _pointCount = extraPoints;

            MobManager.Instance.UpdateMaxMobCount();
        }
    }

    public void AddPoint(float value)
    {
        _pointCount += value;
        _globalPointCount += value;

        CheckStageUpdate();
    }
}
