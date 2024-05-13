using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f; //15m chunk of time

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    float time;

    [SerializeField] float timeScale = 60f;
    [SerializeField] float startAtTime = 28800f; //seconds
    [SerializeField] Text text;
    [SerializeField] Light2D globalLight;
    [SerializeField] Light2D playerSpotlight;
    private int days;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        TimeValueCalculation();
        DayLight();
        if (time > secondsInDay)
        {
            NextDay();
        }

        TimeAgents();
    }


    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(time / phaseLength);
        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
    }

    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if (v > 0.5f)
        {
            playerSpotlight.intensity = Mathf.Lerp(0f, 1f, (v - 0.5f) * 2f);
        }
        else
        {
            playerSpotlight.intensity = 0f;
        }
    }

    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
