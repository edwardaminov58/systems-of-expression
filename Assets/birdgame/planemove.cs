using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planemove : MonoBehaviour
{
    public GameObject bird;
    float startaltitude;
    float horizon;
    public float altitudeoffset = 25f;
    float baselevel;
    float altitude;
    public float threshold;
    float horizontal;
    Material mat;
    public float horizonoffset;
    bool grounded;
    public float speed;
    float y;
    float timeCount = 0.0f;
    float x;
    //Vector2 sphere;
    //Vector2 sphereoffset;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startaltitude = bird.transform.localPosition.y;
        horizon = bird.transform.localPosition.z;
        baselevel = startaltitude;
        horizontal = bird.transform.localPosition.x;
        mat = GetComponent<MeshRenderer>().material;
        


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        altitude = bird.transform.localPosition.y - startaltitude;
        transform.localPosition = new Vector3(transform.localPosition.x, baselevel - y - altitudeoffset, bird.transform.localPosition.z + horizonoffset);
        //altitudeoffset = altitudeoffset + altitude;
        Debug.Log(altitude);
        if (bird.transform.localPosition.y > threshold)
        {
            
            // transform.localPosition = new Vector3(transform.localPosition.x, baselevel - altitude - altitudeoffset, transform.localPosition.z);
            grounded = false;
        }
        else if (bird.transform.localPosition.y < threshold )
            grounded = true;

        if (grounded)
        {
            y = 0;
            Debug.Log("grounded");
        }
        if (!grounded)
        {
            
            y = (bird.transform.localPosition.y - threshold) * speed;
            Debug.Log("not grounded");
        }
        x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 90), rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 35, 90),  rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, -35, 90),  rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        //transform.localRotation = new Vector3()
        //sphere = mat.GetVector("_sphere_offset");
        //sphereoffset = sphere + new Vector2(1, 0);
        //mat.SetVector("_sphere_offset", sphereoffset);
        //mat.SetVector("_offset2", new Vector2(0, -1));   
    }
}
