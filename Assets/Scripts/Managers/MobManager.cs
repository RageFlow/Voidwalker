using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public static MobManager Instance;

    public List<Abstract_Mob_Values> Mobs => _mobs;
    [SerializeField] private List<Abstract_Mob_Values> _mobs;
    
    public List<Abstract_Item_Values> Items => _items;
    [SerializeField] private List<Abstract_Item_Values> _items;

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
