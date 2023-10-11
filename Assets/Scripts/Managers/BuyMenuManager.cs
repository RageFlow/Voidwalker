using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuyMenuManager : MonoBehaviour
{
    public static BuyMenuManager Instance;
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

        if (_buyMenuUIGameObject != null)
        {
            _buyMenuUIGameObject.SetActive(false);
        }
    }

    public bool BuyMenuIsOpen => _buyMenuIsOpen;
    private bool _buyMenuIsOpen;

    public GameObject BuyMenuItemPrefab => _buyMenuItemPrefab;
    [SerializeField] private GameObject _buyMenuItemPrefab;

    [SerializeField] private TabGroup _tabGroup;

    [SerializeField] private GameObject _buyMenuUIGameObject;

    [SerializeField] private TextMeshProUGUI _buyMenuUITotalMoneyText;

    public Color PurchaseableColor => _purchaseableColor;
    [SerializeField] private Color _purchaseableColor;

    public Color NotPurchaseableColor => _notPurchaseableColor;
    [SerializeField] private Color _notPurchaseableColor;

    public UnityEvent OnBuyMenuUpdated;

    private void Start()
    {
        PlayerManager.Instance.OnUpdated.AddListener(UpdateTotalMoney);
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnUpdated.RemoveListener(UpdateTotalMoney);
    }

    public void OpenBuyMenu(bool value)
    {
        if (_buyMenuUIGameObject != null)
        {
            GameManager.Instance.ToggleGamePause(value);

            _buyMenuIsOpen = value;
            _buyMenuUIGameObject.SetActive(value);
            UpdateTotalMoney();
            OnBuyMenuUpdated.Invoke();
        }
    }

    private void UpdateTotalMoney()
    {
        _buyMenuUITotalMoneyText.SetText(PlayerManager.Instance.GetTotalMoney().ToString());
    }

    public void EquipItem(BuyMenuPageItem_Controller item)
    {
        if (item.BuyMenuType == BuyMenuType.Stat)
        {
            // No need for Equipping
        }
        else if (item.BuyMenuType == BuyMenuType.Weapon)
        {
            WeaponManager.Instance.SetActiveWeapon(item.Name);
        }
        else if (item.BuyMenuType == BuyMenuType.Ability)
        {
            AbilityManager.Instance.ActivateAbility(item.Name);
        }

        OnBuyMenuUpdated.Invoke();
    }

    public void BuyItem(BuyMenuPageItem_Controller item)
    {
        if (item != null && (!item.Activated || item.Upgradeable) && PlayerManager.Instance.CanBuyFor(item.Price))
        {
            PlayerManager.Instance.UpdateMoney(item.Price);

            if (item.BuyMenuType == BuyMenuType.Stat)
            {
                Abstract_Stat_Values stat = GetStatFromManagers(item.Name);

                stat.SetStage(stat.Stage + 1);

                var value = stat.DefaultValue * (stat.Stage * stat.UpgradeFactor);

                if (!stat.Inclusive)
                {
                    value = stat.DefaultValue + stat.DefaultValue * (stat.Stage * stat.UpgradeFactor);
                }

                Global_Values.UpdateValue(stat.AffectedValue, value);

                item.UpdateStage(stat.Stage, stat.MaxStages);
                item.UpdateCurrentValue(Global_Values.GetValue(stat.AffectedValue));
                item.UpdatePrice(stat.Price * stat.Stage);
                item.SetUI();

                if (stat.ShouldUpdatePlayer)
                {
                    PlayerManager.Instance.UpdatePlayerValues();
                }
            }
            else if (item.BuyMenuType == BuyMenuType.Weapon)
            {
                Abstract_Weapon_Values weapon = GetWeaponFromManagers(item.Name);
                weapon.SetActive(true);
                item.UpdateActivated(true);
                item.SetUI();
            }
            else if (item.BuyMenuType == BuyMenuType.Ability)
            {
                Abstract_Ability_Class ability = GetAbilityFromManagers(item.Name);
                ability.SetActive(true);
                item.UpdateActivated(true);
                item.SetUI();
            }
        }

        if (item != null && item.Activated)
        {
            EquipItem(item);
        }

        OnBuyMenuUpdated.Invoke();
    }

    private Abstract_Stat_Values GetStatFromManagers(string name)
    {
        Abstract_Stat_Values stat = StatManager.Instance.StatValues.FirstOrDefault(x => x.Name.Equals(name));

        return stat;
    }
    
    private Abstract_Weapon_Values GetWeaponFromManagers(string name)
    {
        Abstract_Weapon_Values weapon = WeaponManager.Instance.Weapons.FirstOrDefault(x => x.Name.Equals(name));

        return weapon;
    }
    
    private Abstract_Ability_Class GetAbilityFromManagers(string name)
    {
        Abstract_Ability_Class ability = AbilityManager.Instance.AbilityValues.FirstOrDefault(x => x.Name.Equals(name));

        return ability;
    }
}
