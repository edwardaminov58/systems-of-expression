using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ProfileSwitcher : MonoBehaviour
{

    public flight Flight;
    public rotation Rotation;
    public float switchTime;
    CameraProfile lastcameraProfile;
    //public Material birdmat;
    public CinemachineVirtualCamera vcam;
     public SpriteRenderer birdSpr;
    //public bool firstPerson;





    public void Switch(CameraProfile cameraProfile)
    {
        lastcameraProfile = Flight.cameraProfile;
        Flight.cameraProfile = cameraProfile;
        Rotation.cameraProfile = cameraProfile;
        StartCoroutine(animateSwitch(cameraProfile));








    }

    IEnumerator animateSwitch(CameraProfile cameraProfile)
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / switchTime) {
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.Lerp(lastcameraProfile.CameraDistance, cameraProfile.CameraDistance, t);
            //if (cameraProfile.clear == true)
            //birdSpr.material.color = Color.clear;
            //else
            //    birdmat.color = Color.white;

        }
        yield return null;
    }
}