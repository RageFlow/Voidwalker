using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabGroup_Button : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private TabGroup _tabGroup;

    public Image Background;

    public UnityEvent OnTabSelected;
    public UnityEvent OnTabDeSelected;

    void Awake()
    {
        Background = GetComponent<Image>();
        _tabGroup = gameObject.transform.parent.gameObject.GetComponent<TabGroup>();
    }

    private void Start()
    {
        _tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tabGroup.OnTabExit(this);
    }

    public void Select()
    {
        if (OnTabSelected != null)
        {
            OnTabSelected.Invoke();
        }
    }
    public void DeSelect()
    {
        if (OnTabDeSelected != null)
        {
            OnTabDeSelected.Invoke();
        }
    }
}
