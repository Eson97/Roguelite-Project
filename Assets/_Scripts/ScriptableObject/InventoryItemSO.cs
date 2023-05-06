using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSO : ScriptableObject
{
    [Header("General Config")]
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected int _price;
    [SerializeField] protected Sprite _uiIcon;
    [SerializeField] protected GameObject _prefab;

    public int Id => _name.GetHashCode();
    public string Name => _name;
    public virtual string Description => _description;
    public int Price => _price;
    public Sprite UIIcon => _uiIcon;
    public GameObject Prefab => _prefab;
}
