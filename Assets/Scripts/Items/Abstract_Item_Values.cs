using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Item/BaseItem")]
public class Abstract_Item_Values : ScriptableObject
{
    public string Name => _name;
    [SerializeField] private string _name;
    
    public float Difficulty => _difficulty;
    [SerializeField] private float _difficulty;
    
    public Sprite ItemSprite => _itemSprite;
    [SerializeField] private Sprite _itemSprite;
    
    public Sprite ItemTripleSprite => _itemTripleSprite;
    [SerializeField] private Sprite _itemTripleSprite;
    
    public Sprite ItemMultiSprite => _itemMultiSprite;
    [SerializeField] private Sprite _itemMultiSprite;
    
    public float Value => _value;
    [SerializeField] private float _value;
    
    public float Amount => _amount;
    [SerializeField] private float _amount;
}
