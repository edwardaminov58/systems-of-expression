using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anglebillboard : MonoBehaviour
{
    public GateData gatedata;
    float distanceFromCamera;
     float x;
     float y;
     float z;
    public bool mirrorX;
    public bool mirrorY;
    public bool mirrorZ;
    float originalY;
    float reflectY;
    // Start is called before the first frame update
    void Start()
    {
        
        //originalY = y;
        //reflectY = y * -1;
    }

    // Update is called once per frame
    void Update()
    {
        reflectY = gatedata.y * -1;
        x = gatedata.x;
        //y = gatedata.y;
        z = gatedata.z;
        transform.forward = Quaternion.Euler(x, y, z) * Camera.main.transform.forward;
        Vector3 heading = transform.position - Camera.main.transform.position;
        distanceFromCamera = Vector3.Dot(heading, Camera.main.transform.forward);

        if (distanceFromCamera < .5)
            Destroy(this.gameObject);

        if (mirrorY == true)
            y = reflectY;
        else if (mirrorY == false)
            y = gatedata.y;


        if (Camera.main.transform.position.x > transform.position.x)
            mirrorY = true;
        else
            mirrorY = false;



    }


}
