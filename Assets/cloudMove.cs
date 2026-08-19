using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMove : MonoBehaviour
{
    public altitudeManager AltitudeManager;
    public float layer1StartY;
    public float layer1EndY;
    public float layer2EndY;
    public float layer3EndY;
    public float layer1StartZ;
    public float layer1EndZ;
    //public float layer2Start;

    public float layer2EndZ;
    //public float layer3Start;

    public float layer3EndZ;
    public GameObject bird;
    public float Threshold;
    public float Zoffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset);
        Ymove();
        Zmove();
    }
    void Ymove()
    {

        Threshold = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer + 1] - AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];
        float altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];

        if (AltitudeManager.currentHeightLayer == 0)
        {

            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(layer1StartY, layer1EndY, t), gameObject.transform.position.z);
        }
        else if (AltitudeManager.currentHeightLayer == 1)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(layer1EndY, layer2EndY, t), gameObject.transform.position.z);

        }
        else if (AltitudeManager.currentHeightLayer == 2)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(layer2EndY, layer3EndY, t), gameObject.transform.position.z);
        }
    }
    void Zmove()
    {
        

        float altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];

        if (AltitudeManager.currentHeightLayer == 0)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            Zoffset = Mathf.Lerp(layer1StartZ, layer1EndZ, t);
        }
        else if (AltitudeManager.currentHeightLayer == 1)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            Zoffset = Mathf.Lerp(layer1EndZ, layer2EndZ, t);

        }
        else if (AltitudeManager.currentHeightLayer == 2)
        {
            float t = (bird.transform.localPosition.y - altStart) / Threshold;
            Zoffset = Mathf.Lerp(layer2EndZ, layer3EndZ, t);
        }
    }

    }
