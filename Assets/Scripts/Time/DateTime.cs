[System.Serializable]
public class DateTime
{
    // Fieds
    private Days day;
    public Days Day => day;
    private int date;
    public int Date => date;
    private int month;
    public int Month => month;
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

    private bool playerCheck;
    public bool PlayerCheck
    {
        get => playerCheck;
        set => playerCheck = value;
    }


    // 타임 설정
    public DateTime(int season, int year, int month, int date, int hour, int minutes)
    {
        day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
        this.date = date;
        this.month = month;
        this.season = (Season)season;
        this.year = year;

        this.hour = hour;
        this.minutes = minutes;

        totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

        totalNumWeeks = 1 + totalNumDays / 7;
    }

    // 타임 이벤트
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
        playerCheck = true;
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

        if (date % 30 == 0)
        {
            AdvanceMonth();
            date = 1;
        }

        totalNumDays++;
    }

    private void AdvanceMonth()
    {
        if (month % 3 == 0)
        {
            AdvanceSeason();
        }
        month++;
    }

    private void AdvanceSeason()
    {
        if (Season == Season.Winter)
        {
            season = Season.Spring;
            month = 0;
            year++;
        }
        else season++;
    }

    // 출력
    public string DateToString() { return $"{year.ToString("D2")} {month} {date}"; }

    public string TimeToString()
    {
        return $"{hour.ToString("D2")} : {minutes.ToString("D2")}";
    }
}