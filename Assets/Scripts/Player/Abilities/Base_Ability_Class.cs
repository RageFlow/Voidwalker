using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Ability_Class : MonoBehaviour
{
    public bool Activated => _activated;
    protected bool _activated;

    public float Cooldown => _cooldown;
    protected float _cooldown = 5f;

    public float TimeRemaining => _TimeRemaining;
    protected float _TimeRemaining;

    public virtual void UseAbility()
    {

    }

    public void ChangeActive(bool value)
    {
        _activated = value;
    }
}
