using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/BaseWeapon")]
public class Abstract_Weapon_Values : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;
    
    public string Type => _type;
    [SerializeField] private string _type;
    
    public string Description => _description;
    [SerializeField] private string _description;

    public float Price => _price;
    [SerializeField] private float _price;

    public bool Activated => _activated;
    [SerializeField] private bool _activated;

    public Color Color => _color;
    [SerializeField] private Color _color;

    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;

    public Vector2 Scale => _scale;
    [SerializeField] private Vector2 _scale;

    public Vector2 MuzzleOffset => _muzzleOffset;
    [SerializeField] private Vector2 _muzzleOffset;

    public float Damage => _damage;
    [SerializeField] private float _damage = 1f;

    public float DefaultDamage => _defaultDamage;
    [SerializeField] private float _defaultDamage = 1f;

    public float MobHits => _mobHits;
    [SerializeField] private float _mobHits;

    public float ProjectileAmount => _projectileAmount;
    [SerializeField] private float _projectileAmount = 1f;

    public float ProjectileSpread => _projectileSpread;
    [SerializeField] private float _projectileSpread = 0f;

    public float Force => _force;
    [SerializeField] private float _force = 1f;

    public float BulletAliveTime => _bulletAliveTime;
    [SerializeField] private float _bulletAliveTime = 1f;

    public float TimeBetweenFiring => _timeBetweenFiring;
    [SerializeField] private float _timeBetweenFiring;

    public float DefaultTimeBetweenFiring => _defaultTimeBetweenFiring;
    [SerializeField] private float _defaultTimeBetweenFiring;

    public GameObject Projectile => _projectile;
    [SerializeField] private GameObject _projectile;

    public void SetActive(bool value)
    {
        _activated = value;
    }

    public Abstract_Weapon_Values Clone()
    {
        return Instantiate(this);
    }
}
