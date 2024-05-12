using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    float time;

    [SerializeField] float timeScale = 60f;
    
    [SerializeField] Text text;
    [SerializeField] Light2D globalLight;
    private int days;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }
    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if(time > secondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
