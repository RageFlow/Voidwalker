using UnityEngine;

public class AI_Mob_Values : MonoBehaviour
{
    // Stats
    public float Health => _health;
    private float _health;

    public bool ShowHealth => _showHealth;
    private bool _showHealth;

    public float Damage => _damage;
    private float _damage;

    public float TimeToAttack => _timeToAttack;
    private float _timeToAttack;

    public float Difficulty => _difficulty;
    private float _difficulty;

    // Gameplay
    public Abstract_Item_Values DroppedItem => _droppedItem;
    private Abstract_Item_Values _droppedItem;

    // Visuals
    public Color Color => _color;
    private Color _color;

    public Sprite Sprite => _sprite;
    private Sprite _sprite;

    public RuntimeAnimatorController AnimatorController => _animatorController;
    private RuntimeAnimatorController _animatorController;


    // Movement
    public float Speed => _speed;
    private float _speed;

    public float ChaseDistance => _chaseDistance;
    private float _chaseDistance;

    public float StunTime => _stunTime;
    private float _stunTime;

    public void SetValues(Abstract_Mob_Values values)
    {
        _health = values.Health;
        _showHealth = values.ShowHealth;

        _damage = values.Damage;
        _timeToAttack = values.TimeToAttack;
        _difficulty = values.Difficulty;

        _droppedItem = values.DroppedItem;

        _color = values.Color;
        _sprite = values.Sprite;
        _animatorController = values.AnimatorController;
        _speed = values.Speed;
        _chaseDistance = values.ChaseDistance;
        _stunTime = values.StunTime;
    }
}
