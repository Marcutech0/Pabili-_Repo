using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeManager.OneMinuteChanged += UpdateTime;
        TimeManager.OneHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OneMinuteChanged -= UpdateTime;
        TimeManager.OneHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
