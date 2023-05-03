using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Harvest/Crop",fileName ="NewCrop")]
public class CropSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [Header("Sprites")]
    [SerializeField] private Sprite _icon;

    [Header("Harvest")]
    [SerializeField] private SeedSO _seed;

    [Header("Economy")]
    [SerializeField] private int _price;

    public string Name => _name;
    public string Description => _description.Replace("<Seed>", _seed.Name);

    public Sprite Icon => _icon;
    
    public SeedSO Seed => _seed;
    
    public int Price => _price;
}