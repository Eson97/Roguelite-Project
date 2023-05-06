using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Harvest/Seed",fileName ="NewSeed")]
public class SeedSO : InventoryItemSO
{
    [Header("Seed Config")]
    [SerializeField] private List<Sprite> _phases;
    [SerializeField] private int _daysToSwitchPhase;
    [SerializeField] CropSO _crop;


    public override string Description => _description.Replace("<Crop>", _crop.Name);
    public List<Sprite> SpritePhases => _phases;
    public int DaysToSwitchPhase => _daysToSwitchPhase;
    public CropSO Crop => _crop;


    public int GetNumberOfPhases() => _phases.Count;
    public int GetLastPhaseValue() => _phases.Count - 1;
    public int GetDaysToFullGrown() => GetNumberOfPhases() * DaysToSwitchPhase;


}
