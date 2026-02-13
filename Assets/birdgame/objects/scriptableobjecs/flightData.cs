using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "flightData", menuName = "ScriptableObjects/flightData")]
public class flightData : ScriptableObject
{
    public float minForward;
    public float maxForward;
    public Vector2 maxlimit;
    public Vector2 minlimit;
    public float turnSpeed;
    public float leanSpeed;
    public float burst;
    public float Gravity;
    public float maxSoar;
    public float dropFromRise;
    public float slowdownMin;
    public float slowdownMax;
    public float speedupMin;
    public float speedupMax;
    //public float reduceFlap = 5;
    public float FlapMin = 10;
    public float speedtimeStart;
    public float speedtimeDuration;
    public float speedtimeStop;
    public float slowtimeStart;
    public float slowtimeDuration;
    public float slowtimeStop;

}
