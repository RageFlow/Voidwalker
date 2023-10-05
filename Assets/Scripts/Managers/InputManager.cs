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

    private Vector2 _move;
    public Vector2 Move => _move;
    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    private Vector2 _mousePosition;
    public Vector2 MousePosition => _mousePosition;
    private void OnMousePosition(InputValue value)
    {
        _mousePosition = value.Get<Vector2>();
    }

    private bool _mouseClick;
    public bool MouseClick => _mouseClick;
    private void OnMouseClick(InputValue value)
    {
        var click = value.Get<float>();

        _mouseClick = click >= 1;
    }
    
    private void OnChangeWeapon(InputValue value)
    {
        WeaponManager.Instance.ChangeWeapon(); // Change Weapon
        Player_Controller.Instance.UpdateHealth(-10f);
    }
}
