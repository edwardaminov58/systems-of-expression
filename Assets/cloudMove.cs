using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMove : MonoBehaviour
{
    public altitudeManager AltitudeManager;
    public float layer1Start;
    public float layer1End;
    //public float layer2Start;
    public float layer2End;
    //public float layer3Start;
    public float layer3End;
    public GameObject bird;
    public float Threshold;
    public float Zoffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset);

        float altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];
       
        if (AltitudeManager.currentHeightLayer == 0)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(layer1Start, layer1End, t), gameObject.transform.position.z);
        }
        else if (AltitudeManager.currentHeightLayer == 1)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(layer1End, layer2End, t), gameObject.transform.position.z);

        }
        else if (AltitudeManager.currentHeightLayer == 2)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(layer2End, layer3End, t), gameObject.transform.position.z);
        }
    }


}
