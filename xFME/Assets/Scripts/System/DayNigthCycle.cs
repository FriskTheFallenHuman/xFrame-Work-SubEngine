using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [Header("Time")]
    public bool realtime = false;
    public float minPerDay = 1;
    [SerializeField] private int date = 1;
    [SerializeField] private float degreesPerMin;
    [SerializeField] private double initialTime;
    public bool isPaused = false;
    [SerializeField] private double pauseTime;
    [SerializeField] private bool isTimePaused = false;
    [SerializeField] private double timeSincePaused;

    [Header("Skybox Settings")]
    [SerializeField] private Transform sky;
    [SerializeField] private Vector3 skyDegree;

    void InitialDegrees()
    {
        degreesPerMin = realtime ? 360f : 360f / minPerDay;
    }

    void InitialTime()
    {
        initialTime = realtime ? 0 : DateTime.Now.TimeOfDay.TotalMinutes;
    }

    public void Start()
    {
        sky = GetComponent<Transform>();

        InitialDegrees();
        InitialTime();
    }

    public void Update()
    {
        if (realtime || !isPaused)
        {
            UpdateContinuous();
        }
        else
        {
            if (!isTimePaused)
            {
                pauseTime = DateTime.Now.TimeOfDay.TotalMinutes;
                isTimePaused = true;
            }
            else
            {
                timeSincePaused = DateTime.Now.TimeOfDay.TotalMinutes - pauseTime;
            }
        }

        sky.transform.rotation = Quaternion.Euler(skyDegree);
    }

    /* When we should pause the time? */
    public void SetPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseTime = DateTime.Now.TimeOfDay.TotalMinutes;
            isTimePaused = true;
        }
        else
        {
            initialTime += timeSincePaused;
            isTimePaused = false;
        }
    }

    /* Keep updating the time, so the actual day and night cycle happens */
    void UpdateContinuous()
    {
        double currentTime = DateTime.Now.TimeOfDay.TotalMinutes;
        skyDegree.x = (float)((currentTime - initialTime) * degreesPerMin) % 360;
        date = 1 + (int)(((currentTime - initialTime) * degreesPerMin) / 360);


        //Debug.Log( "Day: " + date );
    }

    /* Returns the deltatime value subtracted with the initial time*/
    public double ReturnDeltaTime()
    {
        double deltaTime = DateTime.Now.TimeOfDay.TotalMinutes - initialTime;
        initialTime = DateTime.Now.TimeOfDay.TotalMinutes;
        Debug.Log(deltaTime);
        return deltaTime;
    }
}
