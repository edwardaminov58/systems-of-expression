using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundmoveGround : MonoBehaviour
{
    // Start is called before the first frame update
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
    Vector2 tiling;
    float x;
    float timeCount = 0.0f;
    public float rotationSpeed;
    //Vector2 sphere;
    //Vector2 sphereoffset;
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
        transform.localPosition = new Vector3(transform.localPosition.x, baselevel - y - altitudeoffset, horizon + horizonoffset);
        //altitudeoffset = altitudeoffset + altitude;
        Debug.Log(altitude);

        //if (bird.transform.localPosition.x > horizontal)
        //{
        //    float t = bird.transform.localPosition.x / threshold;
        //    mat.SetVector("_offset", new Vector2(Mathf.Lerp(0f, 1f, t), 1.09f));
        //}
        if (bird.transform.localPosition.y > threshold)
        {

            // transform.localPosition = new Vector3(transform.localPosition.x, baselevel - altitude - altitudeoffset, transform.localPosition.z);
            grounded = false;
        }
        else if (bird.transform.localPosition.y < threshold)
            grounded = true;

        if (grounded)
        {
            y = 0;
            Debug.Log("grounded");
            float t = bird.transform.localPosition.y / threshold;
            mat.SetVector("_tiling", new Vector2(1, Mathf.Lerp(8f, 3f, t)));
            
            mat.SetVector("_center", new Vector2(0.5f, Mathf.Lerp(2.8f, 3f, t)));
        }
        if (!grounded)
        {

            y = (bird.transform.localPosition.y - threshold) * speed;
            
            Debug.Log("not grounded");
        }
        x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(270, 90, -90), timeCount * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(280, 90, -90), timeCount * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(260, 90, -90), timeCount * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        //sphere = mat.GetVector("_sphere_offset");
        //sphereoffset = sphere + new Vector2(1, 0);
        //mat.SetVector("_sphere_offset", sphereoffset);
        //mat.SetVector("_offset2", new Vector2(0, -1));   
        tiling = mat.GetVector("_tiling");
    }
}
