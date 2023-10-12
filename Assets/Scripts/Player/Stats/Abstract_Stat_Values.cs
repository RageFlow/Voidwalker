using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Stat", menuName = "Stat/BaseStat")]
public class Abstract_Stat_Values : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    public string Type => _type;
    [SerializeField] private string _type;

    public string Description => _description;
    [SerializeField] private string _description;

    public float Stage => _stage;
    [SerializeField] private float _stage;
    
    public float MaxStages => _maxStages;
    [SerializeField] private float _maxStages;
    
    public float UpgradeFactor => _upgradeFactor;
    [SerializeField] private float _upgradeFactor;
    
    public bool Inclusive => _inclusive;
    [SerializeField] private bool _inclusive;

    public float DefaultValue => _defaultValue;
    [SerializeField] private float _defaultValue;

    public float Price => _price;
    [SerializeField] private float _price;

    public bool Activated => _activated;
    [SerializeField] private bool _activated;
    
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public string AffectedValue => _affectedValue;
    [SerializeField] private string _affectedValue;
    
    public bool ShouldUpdatePlayer => _shouldUpdatePlayer;
    [SerializeField] private bool _shouldUpdatePlayer;

    public void SetActive(bool value)
    {
        _activated = value;
    }
    
    public void SetStage(float value)
    {
        _stage = value;

        if (!_activated && _stage > 0)
        {
            SetActive(true);
        }
        else if (_activated && _stage <= 0)
        {
            SetActive(false);
        }
    }

    public Abstract_Stat_Values Clone()
    {
        return Instantiate(this);
    }
}
