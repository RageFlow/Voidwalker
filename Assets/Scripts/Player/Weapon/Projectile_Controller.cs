using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    private float _force;

    public float Damage => _damage;
    private float _damage;
    
    public float Hits => _hits;
    private float _hits;

    private float _destroyTime = 2f;

    void Awake()
    {
        if (Weapon_Controller.Instance != null)
        {
            _damage = Weapon_Controller.Instance.WeaponValues.Damage * Global_Values.WeaponDamageFactor;
            _hits = Weapon_Controller.Instance.WeaponValues.MobHits * Global_Values.WeaponHitFactor;
            _force = Weapon_Controller.Instance.WeaponValues.Force * Global_Values.WeaponForceFactor;

            _rigidbody2D = GetComponent<Rigidbody2D>();

            var screenSpaceMousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);

            Vector3 direction = screenSpaceMousePosition - transform.position;

            Vector3 rotation = transform.position - screenSpaceMousePosition;

            _rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * _force;

            float floatRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, floatRotation + 90);
        }

        Destroy(gameObject, _destroyTime);
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
