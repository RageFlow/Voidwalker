using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    private float _force;
    private float _damage;
    
    public float Hits => _hits;
    private float _hits;

    private float _directionOffset;

    void Awake()
    {
        Destroy(gameObject, Weapon_Controller.Instance.WeaponValues.BulletAliveTime);
    }

    private void Start()
    {
        if (Weapon_Controller.Instance != null)
        {
            _damage = Weapon_Controller.Instance.WeaponValues.Damage * Global_Values.WeaponDamageFactor;
            _hits = Weapon_Controller.Instance.WeaponValues.MobHits * Global_Values.WeaponHitFactor;
            _force = Weapon_Controller.Instance.WeaponValues.Force * Global_Values.WeaponForceFactor;

            _rigidbody2D = GetComponent<Rigidbody2D>();

            var screenSpaceMousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

            Vector2 direction = (Vector2)screenSpaceMousePosition - (Vector2)transform.position; // Stock

            var vector2 = Quaternion.Euler(0, 0, _directionOffset) * direction.normalized * _force;
            _rigidbody2D.velocity = vector2;

            Vector3 rotation = transform.position - screenSpaceMousePosition;
            float floatRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, floatRotation + 90);
        }
    }

    public void UpdateDirectionOffset(float offset)
    {
        _directionOffset = offset;
    }

    private List<string> hitMobs = new();

    public float UpdateHit(string id)
    {
        if (!hitMobs.Contains(id))
        {
            hitMobs.Add(id);
            _hits--;

            if (_hits <= 0f)
            {
                Destroy(gameObject);
                return _damage;
            }
            else{
                return _damage;
            }
        }
        else
        {
            return 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Projectile" && collider.gameObject.tag != "Mob")
        {
            Destroy(gameObject);
        }
    }
}
