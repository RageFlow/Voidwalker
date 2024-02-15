using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Mob", menuName = "Mob/BaseMob")]
public class Abstract_Mob_Values : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    // Stats
    public float Health => _health;
    [SerializeField] private float _health;

    public bool ShowHealth => _showHealth;
    [SerializeField] private bool _showHealth;

    public float Damage => _damage;
    [SerializeField] private float _damage;
    
    public float TimeToAttack => _timeToAttack;
    [SerializeField] private float _timeToAttack;
    
    public float Difficulty => _difficulty;
    [SerializeField] private float _difficulty;


    // Gameplay
    public Abstract_Item_Values DroppedItem => _droppedItem;
    [SerializeField] private Abstract_Item_Values _droppedItem;


    // Visuals
    public Color Color => _color;
    [SerializeField] private Color _color;
    
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public RuntimeAnimatorController AnimatorController => _animatorController;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    

    // Movement
    public float Speed => _speed;
    [SerializeField] private float _speed;
    
    public float ChaseDistance => _chaseDistance;
    [SerializeField] private float _chaseDistance;
    
    public float StunTime => _stunTime;
    [SerializeField] private float _stunTime;
}
