using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public Abstract_Weapon_Values DefaultWeapon => _defaultWeapon;
    [SerializeField] private Abstract_Weapon_Values _defaultWeapon;
    
    public Abstract_Weapon_Values ActiveWeapon => _activeWeapon;
    private Abstract_Weapon_Values _activeWeapon;

    public List<Abstract_Weapon_Values> Weapons => _weapons;
    [SerializeField] private List<Abstract_Weapon_Values> _weapons;

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

        _activeWeapon = _defaultWeapon;
    }

    private void Start()
    {
        UpdateWeapon();
    }

    public void ChangeWeapon()
    {
        int index = Random.Range(0, _weapons.Count);
        Abstract_Weapon_Values newWeapon = _weapons[index];

        _activeWeapon = newWeapon;
        UpdateWeapon();
    }

    private void UpdateWeapon()
    {
        if (Weapon_Controller.Instance != null)
        {
            Weapon_Controller.Instance.UpdateWeapon(_activeWeapon);
        }
    }
}
