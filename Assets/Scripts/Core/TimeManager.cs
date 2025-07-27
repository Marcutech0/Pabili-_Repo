using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OneMinuteChanged;
    public static Action OneHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        Minute = 0;
        Hour = 10;
        timer = minuteToRealTime;
    }


    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            OneMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OneHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}
