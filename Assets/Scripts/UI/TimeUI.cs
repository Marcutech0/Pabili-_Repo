using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;

    private void OnEnable()
    {
        TimeManager.OneMinuteChanged += UpdateTime;
        TimeManager.OneHourChanged += UpdateTime;
        TimeManager.DayChanged += UpdateDay;
    }

    private void OnDisable()
    {
        TimeManager.OneMinuteChanged -= UpdateTime;
        TimeManager.OneHourChanged -= UpdateTime;
        TimeManager.DayChanged -= UpdateDay;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }

    private void UpdateDay(TimeManager.Days CurrentDay)
    {
        dayText.text = $"{CurrentDay}";
    }
}
