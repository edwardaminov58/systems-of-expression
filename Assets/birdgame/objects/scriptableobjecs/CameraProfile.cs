using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraProfile", menuName = "ScriptableObjects/CameraProfile")]
public class CameraProfile : ScriptableObject
{
    public float nosediveSpeed;
    public float nosediveFMultiplier;
    public float xspeed;
    public float yupspeed;
    public float ydownspeed;
    public float Tiltspeed;
    public float xreturntoNeutralSpeed;
    public float yreturntoNeutralSpeed;
    public float TiltReturnSpeed;
    public float minLens;
    public float maxLens;
    public float CameraDistance;
    public bool clear;
    public float XRotBase;
    public float XRot;
    public float YRotBase;
    public float YRotUp;
    public float YRotDown;
    public float TiltRot ;

}
