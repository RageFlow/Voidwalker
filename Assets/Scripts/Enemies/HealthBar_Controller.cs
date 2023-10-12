using UnityEngine;

public class HealthBar_Controller : MonoBehaviour
{
    private Vector3 _localScale;

    private float _maxScale;

    private AI_Enemy_Controller _enemyController;

    private SpriteRenderer _spriteRenderer;

    private Color _opacity = new Color(0f, 0f, 0f, 255f);

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _enemyController = gameObject.transform.parent.gameObject.GetComponent<AI_Enemy_Controller>();

        if (!Global_Values.ShowHealthbars)
        {
            gameObject.SetActive(false);
        }
        else if (Global_Values.HideHealthbars)
        {
            _spriteRenderer.color = _spriteRenderer.color - _opacity;
        }

        _localScale = transform.localScale;

        _maxScale = _localScale.x;
    }

    void FixedUpdate()
    {
        if (_enemyController != null)
        {
            if (_enemyController.Alive)
            {
                float width = _enemyController.Health / _enemyController.MaxHealth * _maxScale;
                _localScale.x = width;
                transform.localScale = _localScale;
            }
            else
            {
                Object.Destroy(gameObject);
            }
        }

        if (Global_Values.HideHealthbars)
        {
            _spriteRenderer.color = _spriteRenderer.color - _opacity;
        }
        else{
            _spriteRenderer.color = _spriteRenderer.color + _opacity;
        }
    }
}
