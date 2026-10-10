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
    public float colorSpeed;
    public float colorThreshold;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {

        mat = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //mat.SetColor("_alpha", Color.blue);
        // Threshold = (AltitudeManager.altitudes[layer] + Thresholdoffset ) *speed;

        // float altStart = AltitudeManager.altitudes[layer];
        if (bird.transform.position.y > altStart)
        {
            float t = (bird.transform.localPosition.y - altStart) / (Threshold * speed );
            gameObject.transform.rotation = Quaternion.Euler(transform.eulerAngles = new Vector3(Mathf.Lerp(startLerpRotation, 0, t), transform.rotation.y, transform.rotation.z));
            float r = (bird.transform.localPosition.y - altStart) / (colorThreshold * colorSpeed);
            mat.SetColor("_alpha", Color.Lerp(Color.white, Color.black, r));
                //Color.Lerp(Color.white, Color.black, t));
        }
    }
}
