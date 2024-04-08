using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating_rooms : MonoBehaviour
{
    int i;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Float", 0, 10);
        //StartCoroutine("XAxis");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Float ()
    {
        i = Random.Range(0, 2);
        if (i == 0)
        {
            StartCoroutine("XAxis");
            print("xaxis!");
        }
        //else if (i == 1)
        //    StartCoroutine("yAxis");
        //else if (i == 2)
        //    StartCoroutine("zAxis");

        

    }
    IEnumerator XAxis()
    {
        //startTime = Time.time;
        transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, transform.position.x + 20, Time.deltaTime/20), transform.position.y, transform.position.z);
        yield return new WaitForSeconds(3);
        //startTime = Time.time;
        transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, transform.position.x - 20, Time.deltaTime/20), transform.position.y, transform.position.z);
        print("finishedcoroutine");
        
    }



}
