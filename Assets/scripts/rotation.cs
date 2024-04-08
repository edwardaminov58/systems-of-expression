using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class rotation : MonoBehaviour
{
    float timeCount = 0.0f;
    float x;
    public float speed;
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 7f), timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -7f), timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        }

    }
}
