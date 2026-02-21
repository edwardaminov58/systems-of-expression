using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplebillboard : MonoBehaviour
{
    float distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        Vector3 heading = transform.position - Camera.main.transform.position;
        distanceFromCamera = Vector3.Dot(heading, Camera.main.transform.forward);
       // transform.LookAt(Camera.main.transform);

        if (distanceFromCamera < .5)
            Destroy(this.gameObject);
    }
}
