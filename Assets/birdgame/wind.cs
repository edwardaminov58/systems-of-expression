using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    //direction and power of wind ride
    public Vector3 windstrength;
    //how quick it reaches max
    public float rideSpeed;
    //maximum increase of height per second
    public float maxSoar;
    //how quick boost goes back to normal
    public float loseForwardBoost;
    //max boost speed
    public float maxForwardBoost;
    //amount of lost height near wind
    public float sink; 
    //speed of sprite translate near wind
    public float translateSpeedx;
    
    public float translateSpeedy;
    public float translateSpeedret;
    public float rotateSpeed;
    public float windPush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
