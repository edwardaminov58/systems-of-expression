using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float parallaxValueX;
    public float parallaxValueY;

    float startPosX;
    float startPosY;
    float startCamX;
    float startCamY;

    // Start is called before the first frame update
    void OnEnable()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startCamX = Camera.main.transform.position.x;
        startCamY = Camera.main.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(startPosX + ((Camera.main.transform.position.x - startCamX) * -parallaxValueX), startPosY + ((Camera.main.transform.position.y - startCamY) * parallaxValueY), transform.position.z);
    }
}
