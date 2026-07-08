using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundmove : MonoBehaviour
{ public GameObject bird;
    Quaternion startRotation;
    public float Zoffset;
    public float Xoffset;
    float x;
    float timeCount = 0.0f;
    public float rotationSpeed;
    public float returnRotationSpeed;

    // Start is called before the first frame update
    //void Awake()
    //{
    //    transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset);
    //}

    // Update is called once per frame

    private void Start()
    {
        startRotation = Quaternion.Euler(transform.localRotation.eulerAngles);
    }

    void FixedUpdate()
    {


        transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset);
    }
}
