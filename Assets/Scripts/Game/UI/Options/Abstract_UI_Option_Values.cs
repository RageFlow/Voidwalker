using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Option", menuName = "Option/BaseOption")]
public class Abstract_UI_Option_Values : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;

    // Floats
    public float MinFloatValue => _minFloatValue;
    [SerializeField] private float _minFloatValue;
    
    public float MaxFloatValue => _maxFloatValue;
    [SerializeField] private float _maxFloatValue;

    public bool WholeFloatNumber => _wholeFloatNumber;
    [SerializeField] private bool _wholeFloatNumber;

    public float FloatValue => _floatValue;
    [SerializeField] private float _floatValue;

    // Bools
    public bool BoolValue => _boolValue;
    [SerializeField] private bool _boolValue;

    // Globals
    public string AffectedValue => _affectedValue;
    [SerializeField] private string _affectedValue;

    public Abstract_UI_Option_Values Clone()
    {
        return Instantiate(this);
    }
}
