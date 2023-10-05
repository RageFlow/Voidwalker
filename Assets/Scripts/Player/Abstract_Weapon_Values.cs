using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_Weapon_Values : MonoBehaviour
{
    public string Name => _name;
    [SerializeField] private string _name;

    public Color Color => _color;
    [SerializeField] private Color _color;

    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;

    public float Damage => _damage;
    [SerializeField] private float _damage;
    
    public float MobHits => _mobHits;
    [SerializeField] private float _mobHits;
    
    public float Force => _force;
    [SerializeField] private float _force;
    
    public float TimeBetweenFiring => _timeBetweenFiring;
    [SerializeField] private float _timeBetweenFiring;
}
