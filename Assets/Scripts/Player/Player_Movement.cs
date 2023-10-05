using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public static Player_Movement Instance;

    [SerializeField] private float _moveSpeed = 10f;

    private Vector2 _moveDirection = Vector2.zero;

    private Rigidbody2D _rigidbody2D;

    private float _velocityMagnitude;

    public Vector3 PlayerPosition => _lastPosition;
    private Vector3 _lastPosition;

    public bool ShouldBeFlipped => _shouldBeFlipped;
    private bool _shouldBeFlipped;

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

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
    }

    private void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void HandleMovement()
    {
        _velocityMagnitude = (transform.position - _lastPosition).magnitude;

        var newPos = _rigidbody2D.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(newPos);

        _lastPosition = transform.position;
    }
}
