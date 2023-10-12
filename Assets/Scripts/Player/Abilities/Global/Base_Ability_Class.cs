using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Ability_Class : MonoBehaviour
{
    public string AbilityName => _abilityName;
    protected string _abilityName;

    public string AbilityType => _abilityType;
    private string _abilityType;

    public string AbilityDescription => _abilityDescription;
    private string _abilityDescription;

    public bool Activated => _activated;
    protected bool _activated;

    public float Cooldown => _cooldown;
    protected float _cooldown = 5f;

    public float TimeRemaining => _timeRemaining;
    protected float _timeRemaining;

    public bool CanUse => _canUse;
    protected bool _canUse = true;

    public string Key => _key;
    protected string _key;

    private void FixedUpdate()
    {
        if (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
        }
    }

    public float GetCooldownFloat()
    {
        return _timeRemaining / _cooldown; // 0 -> 1
    }

    public virtual void UseAbility()
    {
        _timeRemaining = _cooldown;
    }

    public void ChangeActive(bool value)
    {
        _activated = value;
    }

    public void SetAbilityValues(Abstract_Ability_Class values)
    {
        _abilityName = values.Name;
        _abilityType = values.Type;
        _abilityDescription = values.Description;
        _activated = values.Activated;
        _cooldown = values.Cooldown;
        _key = values.Key;
    }
}
