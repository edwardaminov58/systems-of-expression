using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundmove : MonoBehaviour
{ public GameObject bird;
    public float Zoffset;
    public float Xoffset;
    float x;
    float timeCount = 0.0f;
    public float rotationSpeed = 0.005f;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset);
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset);

       
    }
}
