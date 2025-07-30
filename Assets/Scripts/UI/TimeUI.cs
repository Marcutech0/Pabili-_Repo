using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text dayText;
    public Image timeIcon;

    public Sprite morningIcon;
    public Sprite afternoonIcon;
    public Sprite nightIcon;

    void OnEnable()
    {
        TimeManager.OneMinuteChanged += UpdateTime;
        TimeManager.OneHourChanged += UpdateTime;
        TimeManager.DayChanged += UpdateDay;
    }

    void OnDisable()
    {
        TimeManager.OneMinuteChanged -= UpdateTime;
        TimeManager.OneHourChanged -= UpdateTime;
        TimeManager.DayChanged -= UpdateDay;
    }

    void Start()
    {
        UpdateTime(); // force initial update
        UpdateDay(TimeManager.CurrentDay);
    }

    void UpdateTime()
    {
        int hour = TimeManager.Hour;
        int minute = TimeManager.Minute;
        TimeManager.Days day = TimeManager.CurrentDay;

        UpdateTimeUI(hour, minute, day);
    }

    void UpdateDay(TimeManager.Days day)
    {
        UpdateTimeUI(TimeManager.Hour, TimeManager.Minute, day);
    }

    public void UpdateTimeUI(int hour, int minute, TimeManager.Days day)
    {
        // Update time text
        timeText.text = $"{hour:00}:{minute:00}";
        dayText.text = TimeManager.GetDayName(day); // now shows full name

        // Update icon based on time
        if (hour >= 6 && hour < 12)
        {
            timeIcon.sprite = morningIcon;
        }
        else if (hour >= 12 && hour < 18)
        {
            timeIcon.sprite = afternoonIcon;
        }
        else
        {
            timeIcon.sprite = nightIcon;
        }
    }
}