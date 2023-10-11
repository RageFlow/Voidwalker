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

    public List<Abstract_Ability_Class> AbilityValues => _abilityValues;
    [SerializeField] private List<Abstract_Ability_Class> _abilityValues;
    
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

        if (_abilityValues != null)
        {
            _abilityValues = _abilityValues.Select(x => x.Clone()).ToList();
        }

        _activatedAbilities = new();
    }
    
    private void Start()
    {
        SetAbilities(); // Default all
    }

    private void SetAbilities()
    {
        foreach (var item in AbilityValues)
        {
            SetAbilityByAbstract(item);
        }

        SetAbilitiesUI();
    }

    public void ActivateAbility(string name)
    {
        Base_Ability_Class ability = _activatedAbilities.FirstOrDefault(x => x.AbilityName.Equals(name));
        if (ability != null)
        {
            _activatedAbilities.Remove(ability);
        }
        else
        {
            SetAbilityByAbstract(_abilityValues.FirstOrDefault(x => x.Name.Equals(name)));
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
        var values = AbilityValues.FirstOrDefault(a => a.Name.Equals(item.AbilityName));
        if (values != null)
        {
            var newAbility = Instantiate(AbilityUIPrefab, _abilityUIContainer);
            var controller = newAbility.GetComponent<UI_Ability_Controller>();
            controller.UpdateAbility(item, values.Sprite, values.Key);
        }
    }
}
