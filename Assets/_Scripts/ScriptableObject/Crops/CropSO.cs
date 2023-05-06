using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Harvest/Crop",fileName ="NewCrop")]
public class CropSO : InventoryItemSO
{
    [Header("Crop Config")]
    [SerializeField] private SeedSO _seed;

    public override string Description => _description.Replace("<Seed>", _seed.Name);
    public SeedSO Seed => _seed;
    
}