using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layermove : MonoBehaviour
{
    public GameObject bird;
    //float riseThreshold;
    //float startPosition;
    //public float EndPosition;
    public altitudeManager AltitudeManager;
    //public float riseMaxAlt;
    float layerThreshold;
    public float threshold;
    float startPosition;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position.y;
        layerThreshold = AltitudeManager.altitudes[2];
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (bird.transform.position.y > riseThreshold)
        //{
        //    float t = (bird.transform.position.y - riseThreshold) / riseMaxAlt;
        //    gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(startPosition, EndPosition, t), gameObject.transform.position.z);
        //}

        if (bird.transform.position.y > layerThreshold)
        {
            if (bird.transform.position.y > threshold)
            {
                transform.position = new Vector3 (transform.position.x, ((startPosition - (bird.transform.position.y - threshold) ) * speed), transform.position.z);
            }
        }
    }
}
