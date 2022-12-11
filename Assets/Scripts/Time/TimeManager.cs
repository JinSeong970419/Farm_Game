using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Range(1, 4)] public int _Season;
    [Range(1, 99)] public int _Year;
    [Range(1, 28)] public int _Month;
    [Range(0, 24)] public int _Hour;
    [Range(0, 6)] public int _Minutes;

    public int SecondsIncrease;
    private DateTime DateTime;

    public float TimeBetweenTicks;
    private float currentTikebetweenTicks = 0;

    public static UnityAction<DateTime> OnDateTimeChanged;

    private void Awake()
    {
        DateTime = new DateTime(_Month, _Season - 1, _Year, _Hour, _Minutes * 10);

        //Debug.Log($"Starting Date : {DateTime.StartingDate(2)}");
    }

    private void Start()
    {
        OnDateTimeChanged?.Invoke(DateTime);
    }

    private void Update()
    {
        currentTikebetweenTicks += Time.deltaTime;

        if (currentTikebetweenTicks >= TimeBetweenTicks)
        {
            currentTikebetweenTicks = 0;
            Tick();
        }
    }

    private void Tick()
    {
        AdvanceTime();
    }
    
    private void AdvanceTime()
    {
        DateTime.AdvanceMinutes(SecondsIncrease);

        OnDateTimeChanged?.Invoke(DateTime);
    }

}

[System.Serializable]
public struct DateTime
{
    // Fieds
    private Days day;
    public Days Day => day;
    private int date;
    public int Date => date;
    private int year;
    public int Year => year;

    private int hour;
    public int Hour => hour;
    private int minutes;
    public int Minutes => minutes;

    private Season season;
    public Season Season => season;

    private int totalNumDays;
    public int TotalNumDays => totalNumDays;
    private int totalNumWeeks;
    public int TotalNumWeeks => totalNumWeeks;
    public int currentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;

    // Constructors
    public DateTime(int date, int season, int year, int hour, int minutes)
    {
        day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
        this.date = date;
        this.season = (Season)season;
        this.year = year;

        this.hour = hour;
        this.minutes = minutes;

        totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

        totalNumWeeks = 1 + totalNumDays / 7;
    }

    // Time Advancement
    public void AdvanceMinutes(int SecondsToAdvanceBy)
    {
        if (minutes + SecondsToAdvanceBy >= 60)
        {
            minutes = (minutes + SecondsToAdvanceBy) % 60;
            AdvanceHour();
        }
        else
        {
            minutes += SecondsToAdvanceBy;
        }
    }

    private void AdvanceHour()
    {
        if (hour + 1 == 24)
        {
            hour = 0;
            AdvanceDay();
        }
        else
        {
            hour++;
        }
    }

    private void AdvanceDay()
    {
        day++;

        if (day > (Days)7)
        {
            day = (Days)1;
            totalNumWeeks++;
        }
        else
        {
            day++;
        }

        date++;

        if (date % 29 == 0)
        {
            AdvanceSeason();
            date = 1;
        }

        totalNumDays++;
    }

    private void AdvanceSeason()
    {
        if (Season == Season.Winter)
        {
            season = Season.Spring;
            AdvanceYear();
        }
        else season++;
    }

    private void AdvanceYear()
    {
        date = 1;
        year++;
    }

    // Bool Checks
    public bool IsNight() { return hour > 18 || hour < 6; }
    public bool IsMorning() { return hour >= 6 && hour <= 12; }
    public bool IsAfternoon() { return hour > 12 && hour < 18; }
    public bool IsWeekend() { return day > Days.Fri ? true : false; }
    public bool IsParticularDay(Days _day) { return day == _day; }

    // Key Dates
    public DateTime NewYearsDay(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(1, 0, year, 6, 0);
    }

    public DateTime SummerSolstoce(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(28, 1, year, 6, 0);
    }

    public DateTime PumpkinHarvest(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(28, 2, year, 6, 0);
    }

    // Start Of Season

    // To String
    public override string ToString()
    {
        return $"Date : {DateToString()} Season : {season} Time : {TimeToString()}" + $"\n Total Days : {totalNumDays} | Total Weeks{ totalNumWeeks}";
    }
    public string DateToString() { return $"{Day} {Date} {year.ToString("D2")}"; }

    public string TimeToString()
    {
        int adjustedHour = 0;

        if (hour == 0) { adjustedHour = 12; }
        else if (hour == 24) { adjustedHour = 12; }
        else if (hour >= 13) { adjustedHour = hour - 12; }
        else { adjustedHour = hour; }

        string AmPm = hour == 0 || hour < 12 ? "AM" : "PM";
        return $"{adjustedHour.ToString("D2")} : {minutes.ToString("D2")} {AmPm}";
    }
}

[System.Serializable]
public enum Days
{
    NULL = 0,
    Mon = 1,
    Tue = 2,
    Wed = 3,
    Thu = 4,
    Fri = 5,
    Sat = 6,
    Sun = 7
}

[System.Serializable]
public enum Season
{
    Spring = 0,
    Summer = 1,
    autumn = 2,
    Winter = 3
}
