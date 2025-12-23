using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float parallaxValueX;
    public float parallaxValueY;

    float startPosX;
    float startPosY;
    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(startPosX + (Camera.main.transform.position.x * -parallaxValueX), startPosY + (Camera.main.transform.position.y * parallaxValueY), transform.position.z);
    }
}
