using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyMenuPage_Controller : MonoBehaviour
{
    private List<BuyMenuPageItem_Controller> menuItems = new();

    [SerializeField] private GameObject _pageContent;

    [SerializeField] private BuyMenuType _buyMenuType;

    private void Start()
    {
        BuyMenuManager.Instance.OnBuyMenuUpdated.AddListener(UpdateItems);

        _pageContent.RemoveChildren();
        menuItems.Clear();

        if (_buyMenuType == BuyMenuType.Stat)
        {
            foreach (var stat in StatManager.Instance.StatValues)
            {
                BuyMenuPageItem_Controller component = CreateItem();

                if (component != null)
                {
                    component.UpdateUpgradeable(stat.MaxStages > 1);
                    component.UpdateStage(stat.Stage, stat.MaxStages);
                    component.UpdateCurrentValue(Global_Values.GetValue(stat.AffectedValue));
                    component.SetValues(stat.Activated, stat.Name, stat.Description, stat.Sprite, stat.Price, BuyMenuType.Stat);
                    menuItems.Add(component);
                }
            }
        }
        else if (_buyMenuType == BuyMenuType.Weapon)
        {
            foreach (var weapon in WeaponManager.Instance.Weapons)
            {
                BuyMenuPageItem_Controller component = CreateItem();

                if (component != null)
                {
                    component.SetValues(weapon.Activated, weapon.Name, weapon.Description, weapon.Sprite, weapon.Price, BuyMenuType.Weapon);
                    menuItems.Add(component);
                }
            }
            
        }
        else if (_buyMenuType == BuyMenuType.Ability)
        {
            foreach (var ability in AbilityManager.Instance.AbilityValues)
            {
                BuyMenuPageItem_Controller component = CreateItem();
                if (component != null)
                {
                    component.SetValues(ability.Activated, ability.Name, ability.Description, ability.Sprite, ability.Price, BuyMenuType.Ability);
                    menuItems.Add(component);
                }
            }
        }

        UpdateItems();
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnUpdated.RemoveListener(UpdateItems);
    }

    private void UpdateItems()
    {
        foreach (var item in menuItems)
        {
            if (item.BuyMenuType == BuyMenuType.Stat)
            {
                Abstract_Stat_Values stat = StatManager.Instance.StatValues.FirstOrDefault(x => x.Name.Equals(item.Name));

                item.UpdateActivated(stat.Activated);
                item.UpdateUpgradeable(stat.MaxStages > 1);
                item.UpdateEquipped(false);
                item.UpdatePurchaseable(PlayerManager.Instance.CanBuyFor(item.Price));
                item.SetUI();
            }
            else if (item.BuyMenuType == BuyMenuType.Weapon)
            {
                Abstract_Weapon_Values weapon = WeaponManager.Instance.Weapons.FirstOrDefault(x => x.Name.Equals(item.Name));

                item.UpdateActivated(weapon.Activated);
                item.UpdateEquipped(WeaponManager.Instance.ActiveWeapon.Name.Equals(item.Name));
                item.UpdatePurchaseable(PlayerManager.Instance.CanBuyFor(item.Price));
                item.SetUI();
            }
            else if (item.BuyMenuType == BuyMenuType.Ability)
            {
                Abstract_Ability_Class ability = AbilityManager.Instance.AbilityValues.FirstOrDefault(x => x.Name.Equals(item.Name));

                Base_Ability_Class activatedAbility = AbilityManager.Instance.ActivatedAbilities.FirstOrDefault(x => x.AbilityName.Equals(item.Name));

                item.UpdateActivated(ability.Activated);
                item.UpdateEquipped(activatedAbility != null);
                item.UpdatePurchaseable(PlayerManager.Instance.CanBuyFor(item.Price));
                item.SetUI();
            }
        }
    }

    private BuyMenuPageItem_Controller CreateItem()
    {
        GameObject newItem = Instantiate(BuyMenuManager.Instance.BuyMenuItemPrefab, _pageContent.transform);
        var component = newItem.GetComponent<BuyMenuPageItem_Controller>();

        return component;
    }
}
