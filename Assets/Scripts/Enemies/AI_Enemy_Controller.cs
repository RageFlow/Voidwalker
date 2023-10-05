using System;
using UnityEngine;

public class AI_Enemy_Controller : MonoBehaviour
{
    public float Health => _health;
    private float _health;
    public float MaxHealth => _maxHealth;
    private float _maxHealth;

    private float _damage;

    public bool Alive => _health > 0;

    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;
    private Collider2D _collider2D;

    private Ai_Chase_Controller ai_Chase_Controller;

    private AI_Mob_Values _mob_Values;

    private string m_ID = Guid.NewGuid().ToString();
    public string ID => m_ID;

    private void Awake()
    {
        ai_Chase_Controller = GetComponent<Ai_Chase_Controller>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        _mob_Values = GetComponent<AI_Mob_Values>();

        if (_mob_Values != null)
        {
            _health = _mob_Values.Health;
            _damage = _mob_Values.Damage;
            _spriteRenderer.sprite = _mob_Values.Sprite;

            if (_particleSystem != null)
            {
                var main = _particleSystem.main;
                main.startColor = _mob_Values.Color;
            }
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
            Destroy(gameObject);
        }
    }

    private void AnimateDeath()
    {
        _spriteRenderer.color -= new Color(0f, 0f, 0f, 0.05f); // Crude death anim fade
    }

    private void TakeDamage(float value)
    {
        _health -= value;

        if (!Alive)
        {
            _collider2D.isTrigger = true;
            ai_Chase_Controller.Kill();
            ai_Chase_Controller.enabled = false;
            if (_particleSystem != null)
            {
                _particleSystem.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile")
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
