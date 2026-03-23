using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class benchangle : MonoBehaviour
{
    float distanceFromCamera;
    public GameObject flight;
    public float x;
    public float y;
    public float z;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.forward =  Camera.main.transform.forward + new Vector3(x, y, z);
        Vector3 heading = transform.position - Camera.main.transform.position;
        distanceFromCamera = Vector3.Dot(heading, Camera.main.transform.forward);
        // transform.LookAt(Camera.main.transform);

        if (distanceFromCamera < .5)
            Destroy(this.gameObject);
    }
}
