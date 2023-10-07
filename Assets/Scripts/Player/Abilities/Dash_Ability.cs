using System.Collections;
using UnityEngine;

public class Dash_Ability : Base_Ability_Class
{
    private float _dashPowerFactor = 2f;
    private float _dashTime = 1f;

    public bool CanDash => _canDash;
    private bool _canDash = true;

    private float _localCooldown;

    private void Start()
    {
        _localCooldown = _cooldown; // Juks
        _cooldown += _dashTime;
    }

    public override void UseAbility()
    {
        if (_activated && _canDash && !Player_Movement.Instance.MovementBlocked)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (_TimeRemaining > 0f)
        {
            _TimeRemaining -= Time.deltaTime;
        }
    }

    private IEnumerator Dash()
    {
        _TimeRemaining = _cooldown;
        _canDash = false;
        Player_Movement.Instance.ChangeMovementBlocked(true);
        Player_Movement.Instance.Rigidbody2D.velocity = Player_Movement.Instance.MoveDirection * Player_Movement.Instance.MoveSpeed * _dashPowerFactor;
        Player_Movement.Instance._dustTrail.emitting = true;

        yield return new WaitForSeconds(_dashTime);
        Player_Movement.Instance._dustTrail.emitting = false;
        Player_Movement.Instance.ChangeMovementBlocked(false);

        yield return new WaitForSeconds(_localCooldown);
        _canDash = true;
    }
}
