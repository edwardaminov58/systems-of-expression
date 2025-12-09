using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treemove1 : MonoBehaviour
{
    public GameObject bird;
    float startaltitude;
    float horizon;
    public float altitudeoffset = 25f;
    float baselevel;
    float altitude;
    public float threshold;
    float horizontal;
    public Material mat;
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
    Vector2 center;

    //Vector2 sphere;
    //Vector2 sphereoffset;
    //public float rotationSpeed;
    // Start is called before the first frame update

    private void OnEnable()
    {
        // mat = GetComponent<MeshRenderer>().material;
        startaltitude = bird.transform.position.y;
    }

    void FixedUpdate()
    {
        horizon = bird.transform.position.z;
        altitude = bird.transform.position.y - startaltitude;
        y = (bird.transform.position.y);
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y -altitudeoffset - bird.transform.position.y, bird.transform.position.z + horizonoffset);

        Rotate();
        //transform.rotation = Camera.main.transform.rotation;
        //altitudeoffset = altitudeoffset + altitude;
        //Debug.Log(altitude);

        //if (bird.transform.position.x > horizontal)
        //{
        //    float t = bird.transform.position.x / threshold;
        //    mat.SetVector("_offset", new Vector2(Mathf.Lerp(0f, 1f, t), 1.09f));
        //}
        ChangeUV();
        Rotate();

        Debug.Log("not grounded");


        //sphere = mat.GetVector("_sphere_offset");
        //sphereoffset = sphere + new Vector2(1, 0);
        //mat.SetVector("_sphere_offset", sphereoffset);
        //mat.SetVector("_offset2", new Vector2(0, -1));   

    }
    void Rotate()
    {
        x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * returnRotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 8, 10), Time.deltaTime * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -8, -10), Time.deltaTime * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
    }
    void ChangeUV()
    {
        tiling = mat.GetVector("_tiling");
        center = mat.GetVector("_center");
        float t = bird.transform.position.y / threshold;
        mat.SetVector("_tiling", new Vector2(tiling.x, Mathf.Lerp(tilingYstart, tilingYend, t)));

        mat.SetVector("_center", new Vector2(center.x, Mathf.Lerp(centerYstart, centerYend, t)));
    }
}

