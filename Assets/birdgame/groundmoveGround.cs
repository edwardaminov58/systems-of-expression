using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundmoveGround : MonoBehaviour
{
    // Start is called before the first frame update
    public float altStart;
    Quaternion startRotation;
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
   // Vector2 tiling;
  //  Vector2 tilingStart;
   // Vector2 centerStart;
    float x;
    float timeCount = 0.0f;
    public float rotationSpeed;
    public float returnRotationSpeed;
    public groundmoveSettings groundmoveSettings;
    public altitudeManager AltitudeManager;




    //public float tilingXstart;
    //public float tilingXend;
    //public float tilingYstart;
    //public float tilingYend;
    //public float offsetXStart;
    //public float offsetXEnd;
    //public float offsetYstart;
    //public float offsetYEnd;
    //public float centerXstart;
    //public float centerXend;
    //public float centerYstart;
    //public float centerYend;
    //public float strengthXstart;
    //public float strengthXend;    
    //public float strengthYstart;
    //public float strengthYend ;
    //public float tiling2xStart ;
    //public float tiling2xEnd ;
    //public float tiling2yStart;
    //public float tiling2yEnd ;
    //public float offset2XStart;
    //public float offset2XEnd;
    //public float offset2Ystart;
    //public float offset2YEnd;    
    //public float offsetSphereXStart;
    //public float offsetSphereXEnd;
    //public float offsetSphereYstart;
    //public float offsetSphereYEnd;



    Vector2 center;

    //Vector2 sphere;
    //Vector2 sphereoffset;
    // Start is called before the first frame update
    void OnEnable()
    {
        startaltitude = bird.transform.position.y;
        startRotation = Quaternion.Euler(transform.localRotation.eulerAngles);
        baselevel = startaltitude;
        horizontal = bird.transform.position.x;
        mat = GetComponent<MeshRenderer>().material;
       // tilingStart = mat.GetVector("_tiling");
       // centerStart = mat.GetVector("_center");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeUV();
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
        //tiling = mat.GetVector("_tiling");
    }

    void Rotate()
    {
        x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(startRotation.eulerAngles.x, startRotation.eulerAngles.y, startRotation.eulerAngles.z), Time.deltaTime * returnRotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(startRotation.eulerAngles.x +5, startRotation.eulerAngles.y, startRotation.eulerAngles.z), Time.deltaTime * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
        else if (x > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(startRotation.eulerAngles.x-5, startRotation.eulerAngles.y, startRotation.eulerAngles.z), Time.deltaTime * rotationSpeed);
            timeCount = timeCount + Time.deltaTime;
        }


    }
    void ChangeUV()
    {
        altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];
        threshold = (AltitudeManager.altitudes[AltitudeManager.currentHeightLayer+1] - altStart);
        float t = (bird.transform.position.y - altStart)/ threshold;
        
        //mat.SetVector("_tiling", new Vector2(1, Mathf.Lerp(tilingStart, tilingEnd, t)));
        mat.SetVector("_tiling", Vector2.Lerp(groundmoveSettings.tilingStart, groundmoveSettings.tilingEnd, t));
        Debug.Log("changeUV t = " + t);
        mat.SetVector("_offset", Vector2.Lerp(groundmoveSettings.offsetStart, groundmoveSettings.offsetEnd, t));

        // mat.SetVector("_center", new Vector2(0.5f, Mathf.Lerp(centerYstart, centerYend, t)));
        mat.SetVector("_center", Vector2.Lerp(groundmoveSettings.centerStart, groundmoveSettings.centerEnd, t));
        mat.SetVector("_strength", Vector2.Lerp(groundmoveSettings.StrengthStart, groundmoveSettings.StrengthEnd, t));
        mat.SetVector("_tiling2", Vector2.Lerp(groundmoveSettings.tiling2Start, groundmoveSettings.tiling2End, t));
        mat.SetVector("_offset2", Vector2.Lerp(groundmoveSettings.offset2Start, groundmoveSettings.offset2End, t));
        mat.SetVector("_sphereoffset", Vector2.Lerp(groundmoveSettings.offsetSphereStart, groundmoveSettings.offsetSphereEnd, t));
    }
}
