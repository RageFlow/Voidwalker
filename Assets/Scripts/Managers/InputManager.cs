using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
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
    }

    public Vector2 Move => _move;
    private Vector2 _move;
    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    public Vector2 MousePosition => _mousePosition;
    private Vector2 _mousePosition;
    private void OnMousePosition(InputValue value)
    {
        _mousePosition = value.Get<Vector2>();
    }

    public bool MouseClick => _mouseClick;
    private bool _mouseClick;
    private void OnMouseClick(InputValue value)
    {
        var click = value.Get<float>();

        _mouseClick = click >= 1;
    }
    
    private void OnChangeWeapon(InputValue value)
    {
        WeaponManager.Instance.ChangeWeapon(); // Change Weapon
    }
    
    private void OnEscButton(InputValue value)
    {
        MenuManager.Instance.OnEscButton();
    }
    
    private void OnShiftButton(InputValue value)
    {
        AbilityManager.Instance.Dash_Ability.ChangeActive(true); // Temp
        AbilityManager.Instance.Dash_Ability.UseAbility();
    }
}
