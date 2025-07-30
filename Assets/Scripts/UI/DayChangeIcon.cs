using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeIcon : MonoBehaviour
{
    public Image timeOfDayIcon;
    public Sprite morningIcon;
    public Sprite afternoonIcon;
    public Sprite eveningIcon;

    private void OnEnable()
    {
        TimeManager.OneHourChanged += UpdateTimeOfDayIcon;
    }

    private void OnDisable()
    {
        TimeManager.OneHourChanged -= UpdateTimeOfDayIcon;
    }

    private void Start()
    {
        UpdateTimeOfDayIcon();
    }

    private void UpdateTimeOfDayIcon()
    {
        int hour = TimeManager.Hour;

        if (hour >= 6 && hour < 12)
        {
            timeOfDayIcon.sprite = morningIcon;
        }
        else if (hour >= 12 && hour < 18)
        {
            timeOfDayIcon.sprite = afternoonIcon;
        }
        else
        {
            timeOfDayIcon.sprite = eveningIcon;
        }
    }
}