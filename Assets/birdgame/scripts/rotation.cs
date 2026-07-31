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
    public altitudeManager AltitudeManager;


    public CinemachineVirtualCamera vcam;
    bool turn;
    float TiltL;
    float TiltR;
    public float cameraTiltThreshold = 200;
    float cameraTilt;

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
        if(cameraProfile.third == true)
        {
            float altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];
            if (AltitudeManager.currentHeightLayer == 0)
            {
                //vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0f;
                //vcam.transform.rotation = Quaternion.Euler(0, vcam.transform.rotation.y, vcam.transform.rotation.z);
                cameraTilt = 0;
            }
            else if (AltitudeManager.currentHeightLayer == 1)
            {
                float t = (gameObject.transform.localPosition.y - altStart) / cameraTiltThreshold;
                //vcam.transform.rotation = Quaternion.Euler(Mathf.Lerp(0, 15, t), vcam.transform.rotation.y, vcam.transform.rotation.z);
                //vcam.transform.rotation = Quaternion.Lerp(vcam.transform.rotation, Quaternion.Euler(15, vcam.transform.rotation.y, vcam.transform.rotation.z), t);
                cameraTilt = Mathf.Lerp(0, 15f, t);

            }
            else if (AltitudeManager.currentHeightLayer == 2)
            {
                float t = (gameObject.transform.localPosition.y - altStart) / cameraTiltThreshold;
               // vcam.transform.rotation = Quaternion.Euler(Mathf.Lerp(15, 0, t), vcam.transform.rotation.y, vcam.transform.rotation.z);
                cameraTilt = Mathf.Lerp(15, -2.5f, t);
            }
         
            
        }
        else
        {
            cameraTilt = transform.rotation.x;
        }


        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraTilt, transform.rotation.y, cameraProfile.XRotBase), Time.deltaTime * cameraProfile.xreturntoNeutralSpeed);
            //timeCount = timeCount + Time.deltaTime;

        }
        else if (x < 0)
        {
            turn = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraTilt, transform.rotation.y, cameraProfile.XRot), Time.deltaTime * cameraProfile.xspeed);
            //timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            turn = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraTilt, transform.rotation.y, -cameraProfile.XRot), Time.deltaTime * cameraProfile.xspeed);
            //timeCount = timeCount + Time.deltaTime;
        }
        if (turn == true && x == 0)
        {
            //timeCount = 0;
            turn = false;
        }
        //if (cameraProfile.YRotBase != 0)
       // {
            if (y == 0)
            {
                //Debug.Log("rotbase != 0");
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraProfile.YRotBase, transform.rotation.y, transform.rotation.z), Time.deltaTime * cameraProfile.yreturntoNeutralSpeed);
                //timeCount = timeCount + Time.deltaTime;

            }
            else if (y > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraProfile.YRotDown, transform.rotation.y, transform.rotation.z), Time.deltaTime * cameraProfile.ydownspeed); ;
            }
            else if (y < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-cameraProfile.YRotUp, transform.rotation.y, transform.rotation.z), Time.deltaTime * cameraProfile.yupspeed);
            }
        //}

        if (TiltL > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraTilt, transform.rotation.y, transform.rotation.z + cameraProfile.TiltRot), Time.deltaTime * cameraProfile.Tiltspeed);

        }
        if (TiltR > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraTilt, transform.rotation.y, transform.rotation.z - cameraProfile.TiltRot), Time.deltaTime * cameraProfile.Tiltspeed);
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
