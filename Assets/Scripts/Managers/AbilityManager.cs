using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//NZD WAS HERE
public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance;

    public Transform AbilityUIContainer => _abilityUIContainer;
    [SerializeField] private Transform _abilityUIContainer;

    public List<Abstract_Ability_Class> AbilityAsbtracts => _abilityAsbtracts;
    [SerializeField] private List<Abstract_Ability_Class> _abilityAsbtracts;
    
    public List<Base_Ability_Class> ActivatedAbilities => _activatedAbilities;
    private List<Base_Ability_Class> _activatedAbilities = new();

    public GameObject AbilityUIPrefab;

    public List<GameObject> gameObjects = new();

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
    
    private void Start()
    {
        SetAbilities(); // Default all
    }

    private void SetAbilities()
    {
        foreach (var item in AbilityAsbtracts)
        {
            SetAbilityByAbstract(item);
        }

        SetAbilitiesUI();
    }

    private void SetAbilitiesUI()
    {
        _abilityUIContainer.RemoveChildren(); // Clear UI
        foreach (var item in _activatedAbilities)
        {
            SetAbilityUI(item);
        }
    }

    private void SetAbilityByAbstract(Abstract_Ability_Class item)
    {
        if (item.Activated)
        {
            Type type = System.Type.GetType(item.AbilityClass);

            if (type != null)
            {
                GameObject ability = AddAbilityObject(type);

                Base_Ability_Class abilityComponent = ability.GetComponent<Base_Ability_Class>();
                abilityComponent.SetAbilityValues(item);
                _activatedAbilities.Add(abilityComponent);
            }
        }
    }

    private GameObject AddAbilityObject(Type ability)
    {
        GameObject Ability = new GameObject("Test", ability);
        Ability.transform.parent = transform;
        gameObjects.Add(Ability);

        return Ability;
    }

    private void SetAbilityUI(Base_Ability_Class item)
    {
        var values = AbilityAsbtracts.FirstOrDefault(a => a.Name.Equals(item.AbilityName));
        if (values != null)
        {
            var ko = Instantiate(AbilityUIPrefab, _abilityUIContainer);
            var controller = ko.GetComponent<UI_Ability_Controller>();
            controller.UpdateAbility(item, values.Sprite, values.Key);
        }
    }
}
