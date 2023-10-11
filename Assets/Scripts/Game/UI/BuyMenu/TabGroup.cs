using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabGroup_Button> TabButtons;
    public List<GameObject> TabPages;

    public Sprite TabIdle;
    public Sprite TabHover;
    public Sprite TabActive;

    public TabGroup_Button SelectedTab => _selectedTab;
    private TabGroup_Button _selectedTab;

    private void Start()
    {
        if (TabButtons != null && TabButtons.Count > 0 && TabPages != null && TabPages.Count > 0)
        {
            OnTabSelected(TabButtons.FirstOrDefault());
        }
    }

    public void Subscribe(TabGroup_Button button)
    {
        if (TabButtons == null)
        {
            TabButtons = new List<TabGroup_Button>();
        }

        TabButtons.Add(button);
    }

    public void OnTabEnter(TabGroup_Button button)
    {
        ResetTabs();

        if (_selectedTab == null || button != _selectedTab)
        {
            button.Background.sprite = TabHover;
        }
    }
    public void OnTabExit(TabGroup_Button button)
    {
        ResetTabs();

    }
    public void OnTabSelected(TabGroup_Button button)
    {
        if (_selectedTab != null)
        {
            _selectedTab.DeSelect();
        }

        _selectedTab = button;

        _selectedTab.Select();

        ResetTabs();

        button.Background.sprite = TabActive;

        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < TabPages.Count; i++)
        {
            if (i == index)
            {
                TabPages[i].SetActive(true);
            }
            else
            {
                TabPages[i].SetActive(false);
            }
        }

    }

    public void ResetTabs()
    {
        foreach (TabGroup_Button button in TabButtons)
        {
            if (_selectedTab != null && button == _selectedTab)
            {
                continue;
            }
            button.Background.sprite = TabIdle;
        }
    }
}
