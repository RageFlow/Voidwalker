using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    public static Weapon_Controller Instance;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _muzzle;
    
    [SerializeField] private GameObject _projectile;
    [SerializeField] private bool _canFire;
    private float _timer;
    [SerializeField] private float _timeBetweenFiring;

    public float Damage => _damage;
    private float _damage = 10f;
    private float _defaultDamage = 10f;
    public float MobHits => _mobHits;
    private float _mobHits = 2f;
    public float Force => _force;
    private float _force = 5f;

    // Weapon upgradable
    private float _currentStage = 0f;
    private float _currentTimeBetweenFiring;

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

        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        _currentTimeBetweenFiring = _timeBetweenFiring;
    }

    private void Start()
    {
        UpdateWeapon(); // Temp
    }

    private void Update()
    {
        HandleAnimation();
        Shooting();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameStage > _currentStage && _currentTimeBetweenFiring > 0.05)
        {
            UpdateStage();
            UpdateWeapon(); // Temp while weapon upgrades are not available
        }
    }

    private void UpdateStage()
    {
        _currentStage = GameManager.Instance.GameStage;
        _currentTimeBetweenFiring = _timeBetweenFiring / _currentStage;
    }

    private void UpdateWeapon()
    {
        _damage = _defaultDamage * (GameManager.Instance.GameStage / 2);
    }

    private void Shooting()
    {
        if (!_canFire)
        {
            _timer += Time.deltaTime;
            if (_timer > _currentTimeBetweenFiring)
            {
                _canFire = true;
                _timer = 0;
            }
        }

        if (InputManager.Instance.MouseClick && _canFire)
        {
            _canFire = false;
            Instantiate(_projectile, _muzzle.position, Quaternion.identity, GameManager.Instance.ProjectileSpawnContainer);
        }
    }

    private void HandleAnimation()
    {
        var screenSpaceMousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

        Vector3 rotation = screenSpaceMousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        Vector3 weaponPosition = transform.localPosition;
        Vector3 muzzlePosition = _muzzle.localPosition;

        if (!Player_Movement.Instance.ShouldBeFlipped)
        {
            _muzzle.localPosition = new Vector3(muzzlePosition.x, muzzlePosition.y < 0 ? muzzlePosition.y : muzzlePosition.y * -1f, muzzlePosition.z);
            transform.localPosition = new Vector3(weaponPosition.x < 0 ? weaponPosition.x : weaponPosition.x * -1f, weaponPosition.y, weaponPosition.z);
        }
        else
        {
            _muzzle.localPosition = new Vector3(muzzlePosition.x, muzzlePosition.y > 0 ? muzzlePosition.y : muzzlePosition.y * -1f, muzzlePosition.z);
            transform.localPosition = new Vector3(weaponPosition.x > 0 ? weaponPosition.x : weaponPosition.x * -1f, weaponPosition.y, weaponPosition.z);
        }

        _spriteRenderer.flipY = !Player_Movement.Instance.ShouldBeFlipped;
    }
}
