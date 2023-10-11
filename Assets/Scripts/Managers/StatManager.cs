using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;

    public List<Abstract_Stat_Values> StatValues => _statValues;
    [SerializeField] private List<Abstract_Stat_Values> _statValues;

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

        if (_statValues != null)
        {
            _statValues = _statValues.Select(x => x.Clone()).ToList();
        }
    }

    private void Start()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        foreach (var item in StatValues)
        {
            Global_Values.UpdateValue(item.AffectedValue, item.DefaultValue * item.Stage);
        }
    }
}
