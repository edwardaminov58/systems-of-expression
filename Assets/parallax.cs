using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float parallaxValueX;
    public float parallaxValueY;
    public GameObject bg;
    float startPosX;
    float startCamX;
    float startCamY;
    float bgRelativeY;
    //public float bgStartY;
    Material mat;

    float currentPosX;
    float currentPosY;
    float startPosY;
    public float endPosY;
    public float Ythreshold;
    // Start is called before the first frame update
    void OnEnable()
    {
        //startPosX = transform.localPosition.x;
        startPosY = transform.localPosition.y;
        //startCamX = Camera.main.transform.position.x;
        startCamY = Camera.main.transform.position.y;
        //bgStartY = bg.transform.position.y;
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float t = (Camera.main.transform.position.y - startCamY) / Ythreshold;
        //(bgStartY - bg.transform.position.y +)
        //bgRelativeY = bg.transform.position.y - startPosY;
        //currentPosX = ((Camera.main.transform.position.x - startCamX) * -parallaxValueX);
        //currentPosY = (((Camera.main.transform.position.y - startCamY) * -parallaxValueY) - (bgStartY));
        //transform.localPosition = new Vector3(currentPosX, currentPosY, transform.local.z);

        //mat.SetVector("_sphereoffset", new Vector2(((Camera.main.transform.position.x - startCamX) * parallaxValueX), 0));
        //Debug.Log(("camera pos: " + Camera.main.transform.position.x + "-" + startCamX) + "*" + parallaxValueX + "=" + ((Camera.main.transform.position.x - startCamX) * parallaxValueX));
        mat.SetVector("_sphereoffset", new Vector2(Camera.main.transform.position.x / parallaxValueX, 0));
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(startPosY, endPosY, t), transform.localPosition.z);
    }

    private void Update()
    {
        
    }
}
