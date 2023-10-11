using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyMenuPageItem_Controller : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private TextMeshProUGUI _stageText;

    [SerializeField] private Image _priceImage;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _boughtBackground;
    [SerializeField] private Sprite _hoverBackground;

    [SerializeField] private GameObject _equippedObject;

    private Image _mainBackground;
    private bool _purchaseable;

    public string Name => _name;
    private string _name;
    
    public bool Activated => _activated;
    private bool _activated;
    
    public bool Upgradeable => _upgradeable;
    private bool _upgradeable;

    public float Stage => _stage;
    private float _stage;
    
    public float MaxStage => _maxStage;
    private float _maxStage;
    
    public object CurrentValue => _currentValue;
    private object _currentValue;

    
    public bool Equipped => _equipped;
    private bool _equipped;
    
    public string Description => _description;
    private string _description;
    
    public Sprite Sprite => _sprite;
    private Sprite _sprite;
    
    public float Price => _price;
    private float _price;
    
    public BuyMenuType BuyMenuType => _buyMenuType;
    private BuyMenuType _buyMenuType;

    private void Awake()
    {
        _mainBackground = GetComponent<Image>();
    }

    public void UpdateStage(float value, float maxValue)
    {
        _maxStage = maxValue;
        _stage = value;
    }
    public void UpdateActivated(bool value)
    {
        _activated = value;
    }
    public void UpdateCurrentValue(object value)
    {
        _currentValue = value;
    }
    public void UpdateUpgradeable(bool value)
    {
        _upgradeable = value;
    }
    public void UpdatePurchaseable(bool value)
    {
        _purchaseable = value;
    }
    public void UpdateEquipped(bool value)
    {
        _equipped = value;
    }
    public void UpdatePrice(float value)
    {
        _price = value;
    }

    
    public void IfNotUpgradeable()
    {
        if (_activated)
        {
            _priceText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _priceText.gameObject.transform.parent.gameObject.SetActive(true);
        }

        if (_activated && _equipped)
        {
            _mainBackground.sprite = _boughtBackground;
            _equippedObject.SetActive(true);
        }
        else
        {
            _mainBackground.sprite = _background;
            _equippedObject.SetActive(false);
        }

        _descriptionText.SetText(_description);

        _stageText.gameObject.SetActive(false);
    }

    private void IfUpgradeable()
    {
        _stageText.gameObject.SetActive(true);
        _stageText.SetText($"Stage\n{_stage}/{_maxStage}");

        if (_stage < _maxStage)
        {
            _priceText.gameObject.transform.parent.gameObject.SetActive(true);
            _mainBackground.sprite = _background;
        }
        else
        {
            _mainBackground.sprite = _boughtBackground;
            _priceText.gameObject.transform.parent.gameObject.SetActive(false);
        }

        _descriptionText.SetText($"{(!string.IsNullOrEmpty(_description) ? _description + " - " : "")}Current Value: {_currentValue}");

        _equippedObject.SetActive(false);
    }

    public void SetUI()
    {
        if (Upgradeable)
        {
            IfUpgradeable();
        }
        else
        {
            IfNotUpgradeable();
        }


        if (_purchaseable)
        {
            _priceImage.color = BuyMenuManager.Instance.PurchaseableColor;
        }
        else
        {
            _priceImage.color = BuyMenuManager.Instance.NotPurchaseableColor;
        }

        _image.sprite = _sprite;
        _nameText.SetText(_name);

        _priceText.SetText(_price.ToString());
    }

    public void SetValues(bool activated, string name, string description, Sprite sprite, float price, BuyMenuType buyMenuType)
    {
        _activated = activated;
        _name = name;
        _description = description;
        _sprite = sprite;
        _price = price;
        _buyMenuType = buyMenuType;

        SetUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_activated)
        {
            _mainBackground.sprite = _hoverBackground;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Upgradeable || Upgradeable && _stage < _maxStage)
        {
            BuyMenuManager.Instance.BuyItem(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_activated)
        {
            _mainBackground.sprite = _background;
        }
    }
}
