using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Options_Menu_Controller : MonoBehaviour
{
    private List<UI_Options_Item_Controller> optionItems = new();

    [SerializeField] private GameObject _saveButton;
    [SerializeField] private GameObject _dynamicOptionsContainer;
    private Button _saveButtonComponent;

    [SerializeField] private GameObject _optionsMenuItemPrefab;

    public bool IsOptionsVisible;

    public static UI_Options_Menu_Controller Instance;
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

        _saveButtonComponent = _saveButton.GetComponent<Button>();
    }

    void Start()
    {
        _dynamicOptionsContainer.transform.RemoveChildren();

        foreach (var option in MenuManager.Instance.OptionValues)
        {
            UI_Options_Item_Controller component = CreateItem();

            if (component != null)
            {
                component.SetValues(option);

                optionItems.Add(component);
            }
        }

        _saveButtonComponent.interactable = false;

        gameObject.SetActive(false);
    }

    public void IsVisible(bool value)
    {
        IsOptionsVisible = value;
        gameObject.SetActive(value);
    }

    public void CloseOptionsMenu()
    {
        MenuManager.Instance.ToggleGameMenu();

        ResetToGlobalValues();
    }

    private UI_Options_Item_Controller CreateItem()
    {
        GameObject newItem = Instantiate(_optionsMenuItemPrefab, _dynamicOptionsContainer.transform);
        var component = newItem.GetComponent<UI_Options_Item_Controller>();

        return component;
    }

    public void SetChanged()
    {
        _saveButtonComponent.interactable = true;
    }

    public void UpdateGlobalValues()
    {
        foreach (var item in optionItems)
        {
            if (item.Type == OptionMenuItemType.Slider)
            {
                Global_Values.UpdateValue(item.AffectedValue, item.FloatValue);
            }
            else if (item.Type == OptionMenuItemType.Toggle)
            {
                Global_Values.UpdateValue(item.AffectedValue, item.BoolValue);
            }
        }

        ResetToGlobalValues();
    }

    private void ResetToGlobalValues()
    {
        _saveButtonComponent.interactable = false;

        foreach (var item in optionItems)
        {
            item.SetValue(Global_Values.GetValue(item.AffectedValue));
        }
    }
}
