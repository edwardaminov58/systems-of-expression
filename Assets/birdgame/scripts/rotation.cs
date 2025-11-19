using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class rotation : MonoBehaviour
{
    //float timeCount = 0.0f;
    float x;
    public float speed;
    public float returntoNeutralSpeed;
    public CinemachineVirtualCamera vcam;
    bool turn;
    // Start is called before the first frame update
    void Start()
    {
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * returntoNeutralSpeed);
            //timeCount = timeCount + Time.deltaTime;
            
        }
        else if (x < 0)
        {
            turn = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 7f), Time.deltaTime * speed);
            //timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            turn = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -7f), Time.deltaTime * speed);
            //timeCount = timeCount + Time.deltaTime;
        }
        if (turn == true&& x == 0)
        {
            //timeCount = 0;
            turn = false;
        }
        //Debug.Log(timeCount);
    }
}
