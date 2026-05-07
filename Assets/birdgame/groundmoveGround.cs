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
    public float returnRotationSpeed;
    public float tilingYstart = 8;
    public float tilingYend = 3;
    public float centerYstart = 2.8f;
    public float centerYend = 3f;
    //Vector2 sphere;
    //Vector2 sphereoffset;
    // Start is called before the first frame update
    void OnEnable()
    {
        startaltitude = bird.transform.position.y;

        baselevel = startaltitude;
        horizontal = bird.transform.position.x;
        mat = GetComponent<MeshRenderer>().material;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizon = bird.transform.position.z;
        altitude = bird.transform.position.y - startaltitude;
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y - altitudeoffset - y, transform.position.z);
        //altitudeoffset = altitudeoffset + altitude;
        //Debug.Log(altitude);

        //if (bird.transform.position.x > horizontal)
        //{
        //    float t = bird.transform.position.x / threshold;
        //    mat.SetVector("_offset", new Vector2(Mathf.Lerp(0f, 1f, t), 1.09f));
        //}
        if (bird.transform.position.y > threshold)
        {

            // transform.position = new Vector3(transform.position.x, baselevel - altitude - altitudeoffset, transform.position.z);
            grounded = false;
        }
        else if (bird.transform.position.y < threshold)
            grounded = true;

        if (grounded)
        {
            y = 0;
            //Debug.Log("grounded");
            ChangeUV();
        }
        if (!grounded)
        {

            y = (bird.transform.position.y - threshold) * speed;

            //Debug.Log("not grounded");
        }
        Rotate();
        //sphere = mat.GetVector("_sphere_offset");
        //sphereoffset = sphere + new Vector2(1, 0);
        //mat.SetVector("_sphere_offset", sphereoffset);
        //mat.SetVector("_offset2", new Vector2(0, -1));   
        tiling = mat.GetVector("_tiling");
    }

    void Rotate()
    {
        x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(270, 90, -90), Time.deltaTime * returnRotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(275, 90, -90), Time.deltaTime * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(265, 90, -90), Time.deltaTime * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }


    }
    void ChangeUV()
    {
        float t = bird.transform.position.y / threshold;
        mat.SetVector("_tiling", new Vector2(1, Mathf.Lerp(tilingYstart, tilingYend, t)));

        mat.SetVector("_center", new Vector2(0.5f, Mathf.Lerp(centerYstart, centerYend, t)));
    }
}
