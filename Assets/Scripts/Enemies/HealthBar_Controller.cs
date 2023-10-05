using UnityEngine;

public class HealthBar_Controller : MonoBehaviour
{
    private Vector3 _localScale;

    private float _maxScale;

    private AI_Enemy_Controller _enemyController;

    private void Awake()
    {
        _enemyController = gameObject.transform.parent.gameObject.GetComponent<AI_Enemy_Controller>();
    }

    void Start()
    {
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
    }
}
