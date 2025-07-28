using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OneMinuteChanged;
    public static Action OneHourChanged;
    public static Action<Days> DayChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static Days CurrentDay { get; private set; }

    public enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    private float minuteToRealTime = 0.1f;
    public float timer;

    void Start()
    {
        Minute = 0;
        Hour = 8;
        CurrentDay = Days.Monday;
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
                Minute = 0;
                Hour++;

                if (Hour >= 24)
                {
                    Hour = 0;
                    AdvanceDay();
                }

                //Hour = Hour % 12;
                //if (Hour == 0) Hour = 12;

                OneHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }

    private void AdvanceDay()
    {
        CurrentDay = (Days)(((int)CurrentDay + 1) % 7);
        DayChanged?.Invoke(CurrentDay);
    }

    public static string GetDayName(Days day)
    {
        return day.ToString(); // returns full name like "Monday"
    }

    public bool IsMorning()
    {
        return Hour >= 8 && Hour <= 12;
    }

    public bool IsAfternoon()
    {
        return Hour > 12 && Hour < 18;
    }

    public bool IsEvening()
    {
        return Hour > 18 || Hour < 8;
    }
}

/**[System.Serializable]
public struct DateTime
{
    #region Fields

    private Days day;
    private int hour;
    private int minute;
    #endregion

    #region Properties
    public Days Day => day;
    public int Hour => hour;
    public int Minute => minute;
    #endregion

    #region Constructors
    public DateTime(int date, int hour, int minute)
    {
        this.day = (Days)(date % 7);
        if (day == 0) day = (Days)7;

        this.hour = hour;
        this.minute = minute;
    }

    #endregion

    #region Bool Checks
    public bool IsMorning()
    {
        return hour >= 8 && hour <= 12;
    }
    public bool IsAfternoon()
    {
        return hour > 12 && hour < 18;
    }
    public bool IsEvening()
    {
        return hour > 18 || hour < 6;
    }
    #endregion


}
[System.Serializable]
public enum Days
    {
        Monday, Tuesday, Wednesday,
        Thursday, Friday, Saturday, Sunday
    }**/