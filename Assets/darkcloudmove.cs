using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkcloudmove : MonoBehaviour
{
    public GameObject bird;
    public altitudeManager AltitudeManager;
    public float Threshold;
    public float Thresholdoffset;
    public int layer;
    public float startLerpRotation;
    public float speed;
    public float altStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Threshold = (AltitudeManager.altitudes[layer] + Thresholdoffset ) *speed;
       
       // float altStart = AltitudeManager.altitudes[layer];
        if (bird.transform.position.y > altStart)
        {

            float t = (bird.transform.localPosition.y - altStart) / (Threshold * speed);
            gameObject.transform.rotation = Quaternion.Euler(transform.eulerAngles = new Vector3(Mathf.Lerp(startLerpRotation, 0, t), transform.rotation.y, transform.rotation.z));
        }
    }
}
