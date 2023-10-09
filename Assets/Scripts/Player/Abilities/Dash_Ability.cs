using System.Collections;
using UnityEngine;

public class Dash_Ability : Base_Ability_Class
{
    private float _dashPowerFactor = 2f;
    private float _dashTime = 1f;

    private float _localCooldown;

    private void Start()
    {
        _localCooldown = _cooldown; // Juks
        _cooldown += _dashTime;
        _key = "Shift";
    }

    public override void UseAbility()
    {
        if (_activated && _canUse && !Player_Movement.Instance.MovementBlocked)
        {
            base.UseAbility(); // Base for UseAbility
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        _canUse = false;
        Player_Movement.Instance.RemoveMobCollision(true);
        Player_Movement.Instance.ChangeMovementBlocked(true);
        Player_Movement.Instance.Rigidbody2D.velocity = Player_Movement.Instance.MoveDirection * Player_Movement.Instance.MoveSpeed * _dashPowerFactor;
        Player_Movement.Instance._dustTrail.emitting = true;

        yield return new WaitForSeconds(_dashTime);
        Player_Movement.Instance._dustTrail.emitting = false;
        Player_Movement.Instance.ChangeMovementBlocked(false);
        Player_Movement.Instance.RemoveMobCollision(false);

        yield return new WaitForSeconds(_localCooldown);
        _canUse = true;
    }
}
