using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class rotation : MonoBehaviour
{
    //float timeCount = 0.0f;
    float x;
    float y;
    public CameraProfile cameraProfile;
    

    public CinemachineVirtualCamera vcam;
    bool turn;
    float TiltL;
    float TiltR;
    
    // Start is called before the first frame update
    void Start()
    {
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        TiltL = Input.GetAxis("Left Tilt");
        TiltR = Input.GetAxis("Right Tilt");

        //normal
        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), Time.deltaTime * cameraProfile.xreturntoNeutralSpeed);
            //timeCount = timeCount + Time.deltaTime;

        }
        else if (x < 0)
        {
            turn = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 7f), Time.deltaTime * cameraProfile.xspeed);
            //timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            turn = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, -7f), Time.deltaTime * cameraProfile.xspeed);
            //timeCount = timeCount + Time.deltaTime;
        }
        if (turn == true && x == 0)
        {
            //timeCount = 0;
            turn = false;
        }
        if (y == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, transform.rotation.z), Time.deltaTime * cameraProfile.yreturntoNeutralSpeed);
            //timeCount = timeCount + Time.deltaTime;

        }
        if (y > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(20f, transform.rotation.y, transform.rotation.z), Time.deltaTime * cameraProfile.ydownspeed);
        }        
        if (y < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-20f, transform.rotation.y, transform.rotation.z), Time.deltaTime * cameraProfile.yupspeed);
        }

        if (TiltL > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 80), Time.deltaTime * cameraProfile.Tiltspeed);
        }
        if (TiltR > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 80), Time.deltaTime * cameraProfile.Tiltspeed);
        }
        

        ////sim
        //if (x == 0)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * returntoNeutralSpeed);
        //    //timeCount = timeCount + Time.deltaTime;

        //}
        //else if (x < 0)
        //{
        //    turn = true;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 10f), Time.deltaTime * speed);
        //    //timeCount = timeCount + Time.deltaTime;
        //}
        //else if (x > 0)
        //{
        //    turn = true;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -10f), Time.deltaTime * speed);
        //    //timeCount = timeCount + Time.deltaTime;
        //}
        //if (turn == true&& x == 0)
        //{
        //    //timeCount = 0;
        //    turn = false;
        //}
        //Debug.Log(timeCount);
    }
}
