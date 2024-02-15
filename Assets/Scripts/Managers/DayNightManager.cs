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

    private float Time = 720f; // 0 -> 1440

    private float Step = 1f / 600f;

    private float CurrentLight = 0f;

    private void FixedUpdate()
    {
        if (Time >= 1440f)
        {
            Time = 0;
        }
        else
        {
            Time += 0.5f;
        }

        // 120

        if (Time >= 720f)
        {
            if (Time > 840f && Time < 1440f)
            {
                CurrentLight = (1440 - Time) * Step;
            }
        }
        else
        {
            if (Time > 120f)
            {
                CurrentLight = (Time - 120f) * Step;
            }
        }



        if (light2D != null)
        {
            light2D.intensity = CurrentLight;
        }
    }

    public float GetGameTime()
    {
        return Time;
    } 

    public string GetgameTimeConverted()
    {
        float hours = (float)Math.Floor(Time / 60);

        float minutes = (float)Math.Floor(Time - hours);

        return $"{hours}:{minutes}";
    }
}
