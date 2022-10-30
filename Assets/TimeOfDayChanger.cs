using UnityEngine;

public class TimeOfDayChanger : MonoBehaviour
{
    private LightingFigure _lightingFigure;
    private TimeOfDay _timeOfDay;

    public Light Sun;
    public Material Day;
    public Material Night;
  
    void Start()
    {
        _lightingFigure = GetComponent<LightingFigure>();

        SetDay();
    }

    public void Switch()
    {
        if (_timeOfDay is TimeOfDay.Night)
        {
            SetDay();
        }
        else
        {
            SetNight();
        }
    }

    private void SetNight()
    {
        Sun.gameObject.SetActive(false);
        RenderSettings.skybox = Night;
        _timeOfDay = TimeOfDay.Night;
        _lightingFigure.EnableEmission();
    }

    private void SetDay()
    {
        Sun.gameObject.SetActive(true);
        RenderSettings.skybox = Day;
        _timeOfDay = TimeOfDay.Day;
        _lightingFigure.DisableEmission();
    }

    private enum TimeOfDay
    {
        Day,
        Night
    }
}
