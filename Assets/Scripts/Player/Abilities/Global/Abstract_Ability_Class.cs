using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Ability", menuName = "Ability/BaseAbility")]
public class Abstract_Ability_Class : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;
    
    public bool Activated => _activated;
    [SerializeField] private bool _activated;

    public bool ShowUI => _showUI;
    [SerializeField] private bool _showUI;

    public float Cooldown => _cooldown;
    [SerializeField] private float _cooldown;
    
    public string Key => _key;
    [SerializeField] private string _key;
    
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;

    public string AbilityClass => _abilityClass;
    [SerializeField] private string _abilityClass;
}
