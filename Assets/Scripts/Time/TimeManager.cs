using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Range(0, 9999)] public int _Year;
    [Range(1, 4)] public int _Season;
    [Range(1, 12)] public int _Month;
    [Range(1, 29)] public int _Day;
    [Range(0, 24)] public int _Hour;
    [Range(0, 6)] public int _Minutes;

    public int SecondsIncrease;
    private DateTime DateTime;

    public float TimeBetweenTicks;
    public float timeSpeed;
    private float currentTikebetweenTicks;

    

    public static UnityAction<DateTime> OnDateTimeChanged;

    private void Awake()
    {
        DateTime = new DateTime(_Season-1, _Year, _Month, _Day ,_Hour, _Minutes);
    }

    private void Start()
    {
        OnDateTimeChanged?.Invoke(DateTime);
    }

    private void Update()
    {
        currentTikebetweenTicks += Time.deltaTime * timeSpeed;

        if (currentTikebetweenTicks >= TimeBetweenTicks)
        {
            currentTikebetweenTicks = 0;
            AdvanceTime();
        }
    }

    private void AdvanceTime()
    {
        DateTime.AdvanceMinutes(SecondsIncrease);

        OnDateTimeChanged?.Invoke(DateTime);
    }
}