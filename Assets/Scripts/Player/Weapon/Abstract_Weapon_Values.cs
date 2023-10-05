using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/BaseWeapon")]
public class Abstract_Weapon_Values : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    public Color Color => _color;
    [SerializeField] private Color _color;

    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public Vector2 MuzzleOffset => _muzzleOffset;
    [SerializeField] private Vector2 _muzzleOffset;

    public float Damage => _damage;
    [SerializeField] private float _damage;

    public float DefaultDamage => _defaultDamage;
    [SerializeField] private float _defaultDamage;

    public float MobHits => _mobHits;
    [SerializeField] private float _mobHits;
    
    public float Force => _force;
    [SerializeField] private float _force;
    
    public float TimeBetweenFiring => _timeBetweenFiring;
    [SerializeField] private float _timeBetweenFiring;

    public float DefaultTimeBetweenFiring => _defaultTimeBetweenFiring;
    [SerializeField] private float _defaultTimeBetweenFiring;

    public GameObject Projectile => _projectile;
    [SerializeField] private GameObject _projectile;
}