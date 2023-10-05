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
    
    public float Damage => _damage;
    [SerializeField] private float _damage;
    
    public float Difficulty => _difficulty;
    [SerializeField] private float _difficulty;

    // Gameplay
    public GameObject DroppedGameObject => _droppedGameObject;
    private GameObject _droppedGameObject;

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