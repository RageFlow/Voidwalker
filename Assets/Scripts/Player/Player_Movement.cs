using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public static Player_Movement Instance;

    public Vector2 MoveDirection => _moveDirection;
    private Vector2 _moveDirection = Vector2.zero;

    public bool MovementBlocked => _movementBlocked;
    private bool _movementBlocked;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    private Rigidbody2D _rigidbody2D;

    private CircleCollider2D _objectCollider;

    private float _velocityMagnitude;

    public Vector3 PlayerPosition => _lastPosition;
    private Vector3 _lastPosition;

    public bool ShouldBeFlipped => _shouldBeFlipped;
    private bool _shouldBeFlipped;

    [SerializeField] private Animator _animator;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public TrailRenderer _dustTrail;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance == null)
        {
            Destroy(this);
        }

        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (_dustTrail == null)
        {
            _dustTrail = GetComponentInChildren<TrailRenderer>();
        }

        var colliders = GetComponents<CircleCollider2D>();

        foreach (var collider in colliders)
        {
            if (!collider.isTrigger)
            {
                _objectCollider = collider;
            }
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.GamePaused)
        {
            HandleInput();
            HandleAnimation();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void ChangeMovementBlocked(bool value)
    {
        _movementBlocked = value;
    }
    
    public void RemoveMobCollision(bool value)
    {
        var mob = LayerMask.NameToLayer("Mob");
        if (value) // Remove Mob
        {
            _objectCollider.excludeLayers |= (1 << mob);
        }
        else
        {
            _objectCollider.excludeLayers &= ~(1 << mob);
        }
    }

    private void HandleInput()
    {
        _moveDirection = InputManager.Instance.Move;
    }

    private void HandleAnimation()
    {
        var screenSpaceMousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

        _shouldBeFlipped = transform.position.x < screenSpaceMousePosition.x;
        _spriteRenderer.flipX = _shouldBeFlipped;

        if (_velocityMagnitude < 0.01f)
        {
            _animator.Play("Idle");
        }
        else
        {
            if (_spriteRenderer.flipX)
            {
                _animator.Play("Walk_Right");
                _spriteRenderer.flipX = false;
            }
            else
            {
                _animator.Play("Walk_Left");
            }
        }
    }

    private void HandleMovement()
    {
        if (!_movementBlocked)
        {
            _velocityMagnitude = (transform.position - _lastPosition).magnitude;

            var newPos = _rigidbody2D.position + _moveDirection * Global_Values.MoveSpeed * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(newPos);
        }

        _lastPosition = transform.position;
    }
}
