using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planemove : MonoBehaviour
{
    public GameObject bird;
    float startaltitude;
    float horizon;
    public float altitudeoffset = 25f;
    float baselevel;
    float altitude;

    // Start is called before the first frame update
    void Start()
    {
        startaltitude = bird.transform.localPosition.y;
        horizon = bird.transform.localPosition.z;
        baselevel = startaltitude;



    }

    // Update is called once per frame
    void Update()
    {
        altitude = bird.transform.localPosition.y - startaltitude;
        transform.localPosition = new Vector3(0, baselevel - altitude - altitudeoffset, horizon + 400); ;
            }
}
