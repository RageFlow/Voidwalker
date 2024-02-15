using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance == null)
        {
            Destroy(this);
        }
    }

    [SerializeField]
    private Light2D light2D;

    public bool LightsOn;
    public bool IsDay = true;

    // Maybe settings set variables
    private static float TimeGap = 440f;
    private float TimeSpeed = 1f / 30f;
    private float FlashlightOnBelow = 0.5f;

    // Untouchable variables
    private static float _timeMax = 1440f;
    private float Time = 440f; // 0 -> 1440
    private float Step = 1f / ((_timeMax - TimeGap * 2) / 2);
    private float CurrentLight = 0f;

    private void FixedUpdate()
    {
        if (Time >= _timeMax) // 1440
        {
            Time = 0;
        }
        else
        {
            Time += TimeSpeed; // 0.5
        }

        float dayStart = _timeMax / 2 - TimeGap / 2; // 500
        float dayEnd = dayStart + TimeGap; // 940

        float nightStart = _timeMax - TimeGap / 2; // 1220
        float nightEnd = TimeGap / 2; // 220

        float transitionTime = dayStart - nightEnd;

        // Night - Day
        if (Time >= nightStart || Time <= nightEnd)
        {
            // Pause on Night
            IsDay = false;
        }
        else if (Time >= dayStart && Time <= dayEnd)
        {
            // Pause on Day
            IsDay = true;
        }
        else // Run transition
        {
            // Making sure most of the time is day
            IsDay = true;

            // 940 -> 1220 ( + 1 is failsafe)
            if (Time >= dayStart + 1)
            {
                CurrentLight = (transitionTime - (Time - dayEnd)) * Step;
            }
            // 220 -> 500
            else
            {
                CurrentLight = (Time - TimeGap / 2) * Step;
            }

            if (light2D != null)
            {
                light2D.intensity = CurrentLight;

                // Turn lights on/off based on intensity
                LightsOn = light2D.intensity < FlashlightOnBelow;
            }
        }
    }

    public float GetGameTime()
    {
        return Time;
    } 

    public string GetgameTimeConverted()
    {
        float hours = (float)Math.Floor(Time / 60);

        float minutes = (float)Math.Floor(60 * (Time / 60 - hours));

        return $"{hours}:{minutes:00}";
    }
}
