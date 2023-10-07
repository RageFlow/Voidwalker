using UnityEngine;
using UnityEngine.AI;

public class AI_Chase_Controller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private float _speed;
    private float _distanceBetween;

    private float _stunTime;
    private float _stunTimer;
    private bool _stunned;

    private float _distance;

    public bool ShouldBeFlipped => _shouldBeFlipped;
    private bool _shouldBeFlipped;

    private Animator _animator;

    private AI_Mob_Values _mob_Values;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _mob_Values = GetComponent<AI_Mob_Values>();

        if (_mob_Values != null)
        {
            _speed = _mob_Values.Speed;
            _distanceBetween = _mob_Values.ChaseDistance;
            _stunTime = _mob_Values.StunTime;

            _animator.runtimeAnimatorController = _mob_Values.AnimatorController;

            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.speed = _speed;
        }
        else
        {
            Object.Destroy(gameObject);
            Debug.LogWarning("Mob values was not found!");
        }
    }

    private void FixedUpdate()
    {
        _distance = Vector2.Distance(transform.position, Player_Movement.Instance.PlayerPosition);
        if (CheckStunned() && _distance < _distanceBetween)
        {
            _agent.SetDestination(Player_Movement.Instance.PlayerPosition);

            _shouldBeFlipped = transform.position.x < Player_Movement.Instance.PlayerPosition.x;
        }
    }

    //private void FixedUpdate()
    //{
    //    _distance = Vector2.Distance(transform.position, Player_Movement.Instance.PlayerPosition);

    //    if (CheckStunned() && _distance < _distanceBetween)
    //    {
    //        Vector2 direction = Player_Movement.Instance.PlayerPosition - transform.position;
    //        direction.Normalize();

    //        transform.position = Vector2.MoveTowards(transform.position, Player_Movement.Instance.PlayerPosition, _speed * Time.deltaTime);

    //        _shouldBeFlipped = transform.position.x < Player_Movement.Instance.PlayerPosition.x;

    //        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    //    }
    //}

    private bool CheckStunned()
    {
        if (_stunned && _stunTimer >= _stunTime)
        {
            _animator.speed = 1f; // Resume Animation
            _stunned = false; // Remove Mob stun
            _stunTimer = 0f; // Reset Stun timer
            return true;
        }
        else if(_stunned)
        {
            _animator.speed = 0f; // Pause Animation
            _rigidbody2D.velocity = Vector2.zero; // Stop velocity
            _stunTimer += Time.deltaTime; // Update Stun timer
            return false;
        }
        else{
            return true; // Not stunned
        }
    }

    public void Stun()
    {
        _stunned = true; // Stun Mob
        _stunTimer = 0f; // Reset Stun Timer
    }

    public void Kill()
    {
        GameManager.Instance.AddKill(_mob_Values);
        _rigidbody2D.velocity = Vector2.zero; // Stop Mobs velocity
        _stunned = true; // Stun Mob while death is activated
    }
}
