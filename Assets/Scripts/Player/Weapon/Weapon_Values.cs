using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Values : MonoBehaviour
{
    public string Name => _name;
    private string _name;

    public string Type => _type;
    private string _type;

    public string Description => _description;
    private string _description;

    public float Price => _price;
    private float _price;

    public bool Activated => _activated;
    private bool _activated;

    public Color Color => _color;
    private Color _color;

    public Sprite Sprite => _sprite;
    private Sprite _sprite;

    public Vector2 Scale => _scale;
    private Vector2 _scale;

    public Vector2 MuzzleOffset => _muzzleOffset;
    private Vector2 _muzzleOffset;

    public float Damage => _damage;
    private float _damage;

    public float DefaultDamage => _defaultDamage;
    private float _defaultDamage;

    public float MobHits => _mobHits;
    private float _mobHits;
    
    public float ProjectileAmount => _projectileAmount;
    private float _projectileAmount;
    
    public float ProjectileSpread => _projectileSpread;
    private float _projectileSpread;

    public float Force => _force;
    private float _force;
    
    public float BulletAliveTime => _bulletAliveTime;
    private float _bulletAliveTime;

    public float TimeBetweenFiring => _timeBetweenFiring;
    private float _timeBetweenFiring;

    public float DefaultTimeBetweenFiring => _defaultTimeBetweenFiring;
    private float _defaultTimeBetweenFiring;

    public GameObject Projectile => _projectile;
    private GameObject _projectile;

    public void SetValues(Abstract_Weapon_Values values)
    {
        _name = values.Name;
        _type = values.Type;
        _description = values.Description;
        _price = values.Price;
        _activated = values.Activated;

        _color = values.Color;
        _sprite = values.Sprite;
        _muzzleOffset = values.MuzzleOffset;

        _damage = values.Damage;
        _defaultDamage = values.Damage;

        _mobHits = values.MobHits;
        _force = values.Force;
        _bulletAliveTime = values.BulletAliveTime;
        _projectileAmount = values.ProjectileAmount;
        _projectileSpread = values.ProjectileSpread;

        _timeBetweenFiring = values.TimeBetweenFiring;
        _defaultTimeBetweenFiring = values.TimeBetweenFiring;

        _projectile = values.Projectile;
    }
}
