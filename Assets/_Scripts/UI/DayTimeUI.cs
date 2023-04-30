using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimeUI : MonoBehaviour
{
    [SerializeField] private Image _dayTimeImage;
    [SerializeField] private Image  _dayTimeBackground;


    void Start()
    {
        EnvironmentManager.Instance.OnDayTimeChanged += Environment_OnDayTimeChanged;
    }
    private void OnDestroy()
    {
        if (EnvironmentManager.Instance == null) return;

        EnvironmentManager.Instance.OnDayTimeChanged -= Environment_OnDayTimeChanged;
    }

    private void Environment_OnDayTimeChanged(DayTimeSO dayTime)
    {
        _dayTimeImage.sprite = dayTime.Image;
        _dayTimeBackground.color = dayTime.Background;
    }
}
