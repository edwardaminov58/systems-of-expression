using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_room : MonoBehaviour
{
    // Start is called before the first frame update
    float startTime;
    bool changetime = false;
    //float velocity = 4f;
    //float t;
    void Start()
    {
       
        startTime = Time.time;
        //rb = GetComponent<Rigidbody>();
        //InvokeRepeating("Float", 0f, .5f);
       // InvokeRepeating("Come", 0f, 1f);
    }

    // Update is called once per frame
    //void Float()
    //{  
    //    //transform.position = transform.position + new Vector3(1, 0);

    //}

    void Update()
    {


        //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.SmoothDamp(transform.position.z, 46f, ref velocity, 9f));

        if (transform.position.z < 44f)
        {


            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.SmoothStep(3.5f, 44f, (Time.time - startTime) / 9));
            changetime = true;
        }
        
        if (transform.position.z >= 44f)
        {
            ChangeTime();





            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, 46f, ((Time.time - startTime)) / 1000));
        }
    }

    private void ChangeTime()
    {
        if (changetime == true)
        {
            startTime = Time.time;
            changetime = false;
        }
    }


}


