using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance;

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
        _dash_Ability = GetComponent<Dash_Ability>();
    }

    public Dash_Ability Dash_Ability => _dash_Ability;
    private Dash_Ability _dash_Ability;
}
