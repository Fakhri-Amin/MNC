using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier, startHour;
    [SerializeField]
    private float sunriseHour, sunsetHour;
    [SerializeField]
    private float maxSunLightIntensity, maxMoonLightIntensity;
    
    [SerializeField]
    private Light sunLight, moonLight;
    [SerializeField]
    private Color dayAmbientLight, nightAmbientLight;

    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField] 
    private Image progressBar;
    [SerializeField]
    private AnimationCurve lightChangeCurve;

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    
    private bool isTimePaused;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimePaused)
        {
            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();
        }
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
        
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            TimeSpan elapsedTime = currentTime.TimeOfDay - TimeSpan.FromHours(startHour);

            // Jika waktu lebih kecil dari startHour, tambahkan 24 jam (sehari) untuk menghitung perbedaan waktu.
            if (elapsedTime.TotalHours < 0)
            {
                elapsedTime += TimeSpan.FromHours(24);
            }

            float fillAmount = (float)(elapsedTime.TotalSeconds / TimeSpan.FromHours(24).TotalSeconds);
            progressBar.fillAmount = fillAmount;
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    public void StopTime()
    {
        isTimePaused = true;
    }

    public void ContinueTime()
    {
        isTimePaused = false;
    }
}