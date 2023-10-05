using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    public static Weapon_Controller Instance;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _muzzle;
    
    [SerializeField] private bool _canFire;
    private float _timer;

    public Weapon_Values WeaponValues => _weaponValues;
    private Weapon_Values _weaponValues;

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

        _weaponValues = GetComponent<Weapon_Values>();
    }

    private void Update()
    {
        HandleAnimation();
        Shooting();
    }

    public void UpdateWeapon(Abstract_Weapon_Values weaponValues)
    {
        _weaponValues.SetValues(weaponValues);

        if (_spriteRenderer != null && _muzzle != null)
        {
            _spriteRenderer.sprite = _weaponValues.Sprite;
            _spriteRenderer.color = _weaponValues.Color;
            _muzzle.localPosition = new Vector3(_weaponValues.MuzzleOffset.x, _weaponValues.MuzzleOffset.y, 0f);
        }
    }

    private void Shooting()
    {
        if (!_canFire)
        {
            _timer += Time.deltaTime;
            if (_timer > _weaponValues.TimeBetweenFiring)
            {
                _canFire = true;
                _timer = 0;
            }
        }

        if (_weaponValues != null && !string.IsNullOrEmpty(_weaponValues.Name))
        {
            if (InputManager.Instance.MouseClick && _canFire && _weaponValues.Projectile != null)
            {
                _canFire = false;
                Instantiate(_weaponValues.Projectile, _muzzle.position, Quaternion.identity, GameManager.Instance.ProjectileSpawnContainer);
            }
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
