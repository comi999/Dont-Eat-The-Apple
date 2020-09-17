using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : Singleton<TimeController>
{
    private float minutes = 0;
    private Text timeDisplay;
    private string timeText;

    public int currentDay;
    public int currentHour;
    public int currentMinute;

    private string dayText;
    private string hourText;
    private string minuteText;
    private string ampmText;

    private int previousMinute;

    private void Awake()
    {
        timeDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    { 
        minutes += Time.deltaTime;

        if ((int)minutes > previousMinute)
        {
            previousMinute = (int)minutes;

            currentDay = GetDay();
            currentHour = GetHour();
            currentMinute = GetMinute();

            dayText = currentDay.ToString().PadLeft(2, ' ');
            hourText = (((currentHour + 11) % 12) + 1).ToString().PadLeft(2, ' ');
            minuteText = currentMinute.ToString().PadLeft(2, '0');

            if (currentHour < 12) ampmText = "AM";
            else ampmText = "PM";

            dayText = "Day " + dayText + " - " + hourText + ":" + minuteText + ampmText;

            timeDisplay.text = dayText;
        }
    }

    public int GetDay()
    {
        return (int)(minutes * 0.000694f);
    }

    public int GetHour()
    {
        return (int)((minutes - GetDay() * 1440) * 0.0167f);
    }
    
    public int GetMinute()
    {
        return (int)(minutes - GetDay() * 1440 - GetHour() * 60);
    }

    public string GetTimeString()
    {
        return timeText;
    }

    public void SetTime(int day, int hour, int minute)
    {
        minutes = day * 1440 + hour * 60 + minute;
    }

    public void SkipToNextDay(int hour = 8, int minute = 0)
    {
        SetTime(GetDay() + 1, hour, minute);
    }
}
