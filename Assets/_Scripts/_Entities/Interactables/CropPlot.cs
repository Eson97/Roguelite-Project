using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPlot : BaseInteractable
{
    [SerializeField] private SeedSO _currentSeed;
    [SerializeField] private SpriteRenderer _cropVisual;

    private int _currentPhase = -1;
    private int _phaseProgress = 0;

    private void Start()
    {
        EnvironmentManager.Instance.OnWeekDayChanged += Environment_OnWeekDayChanged;
    }

    private void Environment_OnWeekDayChanged(WeekDaySO weekDay)
    {
        if (_currentPhase < 0) return;

        _phaseProgress++;
        if(_phaseProgress >= _currentSeed.DaysToSwitchPhase)
        {
            _currentPhase++;
            
            if(_currentPhase > _currentSeed.GetLastPhaseValue())
                _currentPhase = _currentSeed.GetLastPhaseValue();

            _phaseProgress = 0;

            UpdateCropVisuals();
        }
    }
    public void UpdateCropVisuals()
    {
        if (_currentPhase < 0) _cropVisual.sprite = null;
        else _cropVisual.sprite = _currentSeed.SpritePhases[_currentPhase];
    }

    public override void Interact()
    {
        if (_currentPhase < 0)
            Sow();
        else if (_currentPhase >= _currentSeed.GetLastPhaseValue())
            Harvest();
        else
            GetStatus();
    }
    public void Sow()
    {
        Debug.Log($"Sembrando:\n{_currentSeed.Name}\n{_currentSeed.Description}");
        _currentPhase = 0;
        _phaseProgress = 0;
        UpdateCropVisuals();
    }
    public void Harvest()
    {
        Debug.Log($"Has recolectado {_currentSeed.Crop.Name}\n{_currentSeed.Crop.Description}");
        _currentPhase = -1;
        _phaseProgress = 0;
        UpdateCropVisuals();
    }
    private void GetStatus()
    {
        Debug.Log($"Current phase: {_currentPhase}/{_currentSeed.GetLastPhaseValue()}");
    }
}
