using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public static MobManager Instance;

    public List<Abstract_Mob_Values> Mobs => _mobs;
    [SerializeField] private List<Abstract_Mob_Values> _mobs;

    public Transform MobContainer => _mobContainer;
    [SerializeField] private Transform _mobContainer;

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
}
