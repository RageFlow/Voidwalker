using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Options_Item_Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _optionName;

    // Button
    [SerializeField] private Button _optionButton;
    [SerializeField] private TextMeshProUGUI _optionButtonText;

    // Slider
    [SerializeField] private Slider _optionSlider;
    [SerializeField] private TextMeshProUGUI _optionSliderValue;
    
    // Toggle
    [SerializeField] private Toggle _optionToggle;
    [SerializeField] private TextMeshProUGUI _optionToggleValue;

    public string Name => _name;
    private string _name;

    public OptionMenuItemType Type => _type;
    private OptionMenuItemType _type;

    // Floats
    public float MinFloatValue => _minFloatValue;
    private float _minFloatValue;
    
    public float MaxFloatValue => _maxFloatValue;
    private float _maxFloatValue;
    
    public bool WholeFloatNumber => _wholeFloatNumber;
    private bool _wholeFloatNumber;

    public float FloatValue => _floatValue;
    private float _floatValue;

    // Bools
    public bool BoolValue => _boolValue;
    [SerializeField] private bool _boolValue;

    // Globals
    public string AffectedValue => _affectedValue;
    private string _affectedValue;

    private bool OptionIsLoaded;

    public void SetValues(Abstract_UI_Option_Values values)
    {
        _name = values.Name;

        _minFloatValue = values.MinFloatValue;
        _maxFloatValue = values.MaxFloatValue;
        _wholeFloatNumber = values.WholeFloatNumber;
        _floatValue = values.FloatValue;

        _boolValue = values.BoolValue;

        _affectedValue = values.AffectedValue;

        _optionName.SetText(_name);

        _optionButton.gameObject.SetActive(false); // Disable Button
        _optionSlider.gameObject.SetActive(false); // Disable Slider
        _optionToggle.gameObject.SetActive(false); // Disable Toggle

        SetTypeAndValue();

        if (_type == OptionMenuItemType.Slider)
        {
            _optionSlider.gameObject.SetActive(true); // Enable Slider

            _optionSlider.wholeNumbers = _wholeFloatNumber;
            _optionSlider.maxValue = _maxFloatValue;
            _optionSlider.minValue = _minFloatValue;

            _optionSlider.value = _floatValue;
            _optionSliderValue.SetText(_floatValue.ToString());
        }
        else if (_type == OptionMenuItemType.Button)
        {
            _optionButton.gameObject.SetActive(true); // Enable Button
        }
        else if (_type == OptionMenuItemType.Toggle)
        {
            _optionToggle.gameObject.SetActive(true);
            _optionToggle.isOn = _boolValue;
            _optionToggleValue.SetText(_boolValue.ToString());
        }

        OptionIsLoaded = true;
    }

    private void SetTypeAndValue()
    {
        object value = Global_Values.GetValue(_affectedValue);

        if (value != null)
        {
            if (value is float)
            {
                _type = OptionMenuItemType.Slider;

                Global_Values.UpdateValue(_affectedValue, _floatValue);
            }
            else if (value is bool)
            {
                _type = OptionMenuItemType.Toggle;

                Global_Values.UpdateValue(_affectedValue, _boolValue);
            }
        }
    }

    public void UpdateFloatValue(float value)
    {
        if (OptionIsLoaded)
        {
            UpdateValue(typeof(float));
            SomeValueChanged();
        }
    }
    
    public void UpdateBoolValue(bool value)
    {
        if (OptionIsLoaded)
        {
            UpdateValue(typeof(bool));
            SomeValueChanged();
        }
    }

    public void UpdateValue(Type typeValue)
    {
        if(typeValue == typeof(float))
        {
            _floatValue = _optionSlider.value;
            _optionSliderValue.SetText(_floatValue.ToString());
        }
        else if (typeValue == typeof(bool))
        {
            _boolValue = _optionToggle.isOn;
            _optionToggleValue.SetText(_boolValue.ToString());
        }
    }

    public void SetValue(object value)
    {
        if (value is float floatValue && floatValue != _floatValue)
        {
            _floatValue = floatValue;
            _optionSlider.value = _floatValue;
        }
        else if (value is bool boolValue && boolValue != _boolValue)
        {
            _boolValue = boolValue;
            _optionToggle.isOn = _boolValue;
        }
    }

    public void SomeValueChanged()
    {
        MenuManager.Instance.OptionsChanged();
    }
}
