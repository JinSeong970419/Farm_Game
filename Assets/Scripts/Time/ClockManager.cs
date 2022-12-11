using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class ClockManager : MonoBehaviour
{
    public RectTransform _ClockFace;
    public TextMeshProUGUI _Data, _Time, _Season, _Week;

    //public Image weatherSprite;
    //public Sprite[] weatherSprites;

    private float startingRotation;

    public Light2D sunlight;
    public float nightInstensity;
    public float dayIntensity;

    public AnimationCurve dayNightCurve;

    private bool lerpUp = true;
    // 임시
    public TimeManager TimeManager;

    private void Awake()
    {
        startingRotation = _ClockFace.localEulerAngles.z;
    }

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
        _Data.text = dateTime.DateToString();
        _Time.text = dateTime.TimeToString();
        _Season.text = dateTime.Season.ToString();
        _Week.text = $"WK:{dateTime.currentWeek}";
        //weatherSprite.sprite = weatherSprites[(int)WeatherManager.currentWeather];

        float t = (float)dateTime.Hour / 24f;

        float newRotation = Mathf.Lerp(0, 360, t);
        _ClockFace.localEulerAngles = new Vector3(0, 0, newRotation + startingRotation);

        if (t > 0.5) lerpUp = !lerpUp;

        float dayNightT = dayNightCurve.Evaluate(t);

        sunlight.intensity = Mathf.Lerp(nightInstensity, dayIntensity, dayNightT);
    }
}
