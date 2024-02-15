using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Env_light_control : MonoBehaviour
{
    private Light2D _light;

    //private float _intensity = 0;

    void Start()
    {
        _light = GetComponent<Light2D>();
        //_intensity = _light.intensity;
    }

    void FixedUpdate()
    {
        if (_light != null && DayNightManager.Instance != null)
        {
            _light.enabled = DayNightManager.Instance.LightsOn;
        }
    }
}
