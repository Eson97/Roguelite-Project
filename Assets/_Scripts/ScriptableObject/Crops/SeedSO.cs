using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Harvest/Seed",fileName ="NewSeed")]
public class SeedSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [Header("Sprites")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<Sprite> _phases;

    [Header("Harvest")]
    [SerializeField] private int _daysToSwitchPhase;
    [SerializeField] CropSO _crop;


    [Header("Economy")]
    [SerializeField] private int _price;


    public string Name => _name;
    public string Description => _description.Replace("<Crop>", _crop.Name);
    public Sprite Icon => _icon;
    public List<Sprite> SpritePhases => _phases;
    public int DaysToSwitchPhase => _daysToSwitchPhase;
    public CropSO Crop => _crop;
    public int Price => _price;


    public int GetNumberOfPhases() => _phases.Count;
    public int GetLastPhaseValue() => _phases.Count - 1;
    public int GetDaysToFullGrown() => GetNumberOfPhases() * DaysToSwitchPhase;


}
