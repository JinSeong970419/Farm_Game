using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class ClockManager : MonoBehaviour
{
    public TextMeshProUGUI _Data, _DateWeek ,_Time, _Season, _Week;

    public Light2D sunlight;
    public float nightInstensity;
    public float dayIntensity;

    public AnimationCurve dayNightCurve;

    // 임시
    public TimeManager TimeManager;

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }
    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }
    
    private void UpdateDateTime(DateTime dateTime)
    {
        _Time.text = dateTime.TimeToString();
        _Data.text = dateTime.DateToString();
        _DateWeek.text = dateTime.Day.ToString();
        _Season.text = dateTime.Season.ToString();

        float t = (float)dateTime.Hour / 24f;

        float dayNightT = dayNightCurve.Evaluate(t);

        sunlight.intensity = Mathf.Lerp(nightInstensity, dayIntensity, dayNightT);

        if (dateTime.PlayerCheck) 
        {
            dateTime.PlayerCheck = false;
            GameManager.instance.Health.HP -= 1f; 
        }
    }
}
