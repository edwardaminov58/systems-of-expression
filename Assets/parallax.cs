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
    float bgStartY;

    float currentPosX;
    float currentPosY;

    public float endPosY;

    // Start is called before the first frame update
    void OnEnable()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startCamX = Camera.main.transform.position.x;
        startCamY = Camera.main.transform.position.y;
        bgStartY = bg.transform.position.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //(bgStartY - bg.transform.position.y +)
        //bgRelativeY = bg.transform.position.y - startPosY;
        currentPosX = startPosX + ((Camera.main.transform.position.x - startCamX) * -parallaxValueX);
        currentPosY = Mathf.Clamp(startPosY + ((Camera.main.transform.position.y - startCamY) * parallaxValueY), startPosY, endPosY) - (bgStartY - bg.transform.position.y);
        transform.position = new Vector3(currentPosX, currentPosY, transform.position.z);
        
    }

    private void Update()
    {
        
    }
}
