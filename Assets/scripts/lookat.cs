using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    bool center = true;
    public Transform target;
    Transform cameraPosition;
    Transform startPosition;
    private float startTime;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((-2.3f < target.position.x) && (target.position.x < 4.7f) && (-2.3f < target.position.y) && (target.position.y < 1.5f))
        {
            center = true;
            cameraPosition = gameObject.transform;
        }
        else
            center = false;

        if (center != true)
        {
            print("off");
            Move();
        }        
    }

    void Move() { 
        startTime = Time.time;

        //transform.position = Vector3.Lerp(cameraPosition.position, target.localPosition, ((Time.time - startTime) * speed) / (cameraPosition.position.x- target.position.x));
        //transform.position = target.localPosition;
        //transform.position.x = Vector3.Lerp(cameraPosition.position, target.position, 1);
    }

}
