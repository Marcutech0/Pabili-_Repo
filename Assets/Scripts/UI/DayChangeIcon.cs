using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeIcon : MonoBehaviour
{
    public Image timeOfDayIcon; 
    public TMP_Sprite morningIcon;
    public TMP_Sprite afternoonIcon;
    public TMP_Sprite eveningIcon;

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
        
    }
}
