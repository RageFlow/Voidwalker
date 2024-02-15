using System;
using UnityEngine;

public class AI_Enemy_Controller : MonoBehaviour
{
    public float Health => _health;
    private float _health;
    public float MaxHealth => _maxHealth;
    private float _maxHealth;
    public bool ShowHealth => _showHealth;
    private bool _showHealth;

    private float _damage;

    public bool Alive => _health > 0;

    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;
    private Collider2D _cirkleCollider2D;
    private CapsuleCollider2D _capsuleCollider2D;

    private AI_Chase_Controller ai_Chase_Controller;

    private AI_Mob_Values _mob_Values;

    private string m_ID = Guid.NewGuid().ToString();
    public string ID => m_ID;

    private float _attackTimer = 0f;
    private bool _playerIsInRange;

    private void Awake()
    {
        ai_Chase_Controller = GetComponent<AI_Chase_Controller>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cirkleCollider2D = GetComponent<CircleCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        _mob_Values = GetComponent<AI_Mob_Values>();

        if (_mob_Values != null)
        {
            _health = _mob_Values.Health;
            _showHealth = _mob_Values.ShowHealth;
            _damage = _mob_Values.Damage;
            _spriteRenderer.sprite = _mob_Values.Sprite;

            if (_particleSystem != null)
            {
                var main = _particleSystem.main;
                main.startColor = _mob_Values.Color;
            }

            MobManager.Instance.AddMobToMoblist(ID);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Mob values was not found!");
        }

        if (_health <= 0) // Failsafe
        {
            _health = 10;
        }
        _maxHealth = _health;
    }

    void FixedUpdate()
    {
        if (Alive)
        {
            _spriteRenderer.flipX = !ai_Chase_Controller.ShouldBeFlipped; // Make mob look at player (X Axis)

            if (_attackTimer >= _mob_Values.TimeToAttack)
            {
                Player_Controller.Instance.UpdateHealth(_mob_Values.Damage * -1);
                _attackTimer = 0f;
            }
            else if (_playerIsInRange)
            {
                _attackTimer += Time.deltaTime;
            }
        }
        else
        {
            AnimateDeath();
            Death();
        }
    }

    private void Death()
    {
        if (!Alive && _spriteRenderer.color.a <= 0f) // Destroy if Crude Anime Fade is done
        {
            if (MobManager.Instance != null)
            {
                MobManager.Instance.RemoveMobFromMoblist(ID);
            }
            Destroy(gameObject);
        }
    }

    private void SpawnDrop()
    {
        ItemManager.Instance.SpawnItem(transform.position, _mob_Values.DroppedItem);
    }

    private void AnimateDeath()
    {
        _spriteRenderer.color -= new Color(0f, 0f, 0f, 0.05f); // Crude death anim fade
    }

    private void TakeDamage(float value)
    {
        _health -= value;

        if (!Alive && value != 0f)
        {
            _capsuleCollider2D.enabled = false;
            _cirkleCollider2D.enabled = false;
            ai_Chase_Controller.Kill();
            ai_Chase_Controller.enabled = false;
            if (_particleSystem != null)
            {
                _particleSystem.Play();
            }

            SpawnDrop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            _playerIsInRange = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            _playerIsInRange = false;
            _attackTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_health > 0f && collider.gameObject.tag == "Projectile")
        {
            ai_Chase_Controller.Stun();

            var component = collider.gameObject.GetComponent<Projectile_Controller>();

            if (component != null)
            {
                if (component.Hits > 0f)
                {
                    var hitDamage = component.UpdateHit(ID);
                    TakeDamage(hitDamage);
                }
            }
        }
    }
}
