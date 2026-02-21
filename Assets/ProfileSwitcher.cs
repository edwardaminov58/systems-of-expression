using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ProfileSwitcher : MonoBehaviour
{
    public GameObject shadow;
    public flight Flight;
    public rotation Rotation;
    //public float switchTime;
    //CameraProfile lastcameraProfile;
    //public Material birdmat;
    //public CinemachineVirtualCamera vcam;
    //public Animator bird;
    //public bool firstPerson;
    public CameraProfile first;
    public CameraProfile third;
    //public Animator camera;
    public SpriteRenderer spriteRenderer;

    //private void Update()
    //{
    //    if (firstPerson == true && Flight.cameraProfile != first)
    //    {
            
    //        bird.SetTrigger("clear");
    //        camera.SetTrigger("first");
            
    //    }
    //    else if (firstPerson == false && Flight.cameraProfile == first)
    //    {
    //        bird.SetTrigger("appear");
    //        camera.SetTrigger("third");     
            
    //    }
    //}


    public void Switch(CameraProfile cameraProfile)
    {

        //lastcameraProfile = Flight.cameraProfile;
        Flight.cameraProfile = cameraProfile;
        Rotation.cameraProfile = cameraProfile;
        // StartCoroutine(animateSwitch(cameraProfile));
        
        








    }

    public void Invisible()
    {
        
            spriteRenderer.enabled = false;
       
    }

    public void Visible()
    {
  
            spriteRenderer.enabled = true;
    }

    public void ShadowOn()
    {
        shadow.SetActive(true);
    }  public void ShadowOff()
    {
        shadow.SetActive(false);
    }
    //IEnumerator animateSwitch(CameraProfile cameraProfile)
    //{
    //    for (float t = 0f; t < 1f; t += Time.deltaTime / switchTime) {
    //        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.Lerp(lastcameraProfile.CameraDistance, cameraProfile.CameraDistance, t);
    //        //if (cameraProfile.clear == true)
    //        birdSpr.material.color = Color.clear;
    //        //else
    //        //    birdmat.color = Color.white;
    //        yield return null;
    //    }
       
    //}
}