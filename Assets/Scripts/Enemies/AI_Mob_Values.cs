using UnityEngine;

public class AI_Mob_Values : MonoBehaviour
{
    // Stats
    public float Health => _health;
    private float _health;

    public float Damage => _damage;
    private float _damage;

    public float Difficulty => _difficulty;
    private float _difficulty;

    // Gameplay
    public GameObject DroppedGameObject => _droppedGameObject;
    private GameObject _droppedGameObject;

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
        _damage = values.Damage;
        _difficulty = values.Difficulty;
        _color = values.Color;
        _sprite = values.Sprite;
        _animatorController = values.AnimatorController;
        _speed = values.Speed;
        _chaseDistance = values.ChaseDistance;
        _stunTime = values.StunTime;
    }
}
