using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float parallaxValueX;
    public float parallaxValueY;
    public GameObject bg;
    float startPosX;
    float startPosY;
    float startCamX;
    float startCamY;
    float bgRelativeY;
    public float bgStartY;

    float currentPosX;
    float currentPosY;

    public float endPosY;

    // Start is called before the first frame update
    void OnEnable()
    {
        startPosX = transform.localPosition.x;
        startPosY = transform.localPosition.y;
        startCamX = Camera.main.transform.position.x;
        startCamY = Camera.main.transform.position.y;
        //bgStartY = bg.transform.position.y;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //(bgStartY - bg.transform.position.y +)
        //bgRelativeY = bg.transform.position.y - startPosY;
        currentPosX = ((Camera.main.transform.position.x - startCamX) * -parallaxValueX);
        currentPosY = (((Camera.main.transform.position.y - startCamY) * -parallaxValueY) - (bgStartY));
        transform.localPosition = new Vector3(currentPosX, currentPosY, transform.localPosition.z);
        
    }

    private void Update()
    {
        
    }
}
