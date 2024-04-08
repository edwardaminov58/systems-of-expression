using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{
    public GameObject[] waypoint;
    public int num = 0;

    public float minZDist;
    public float speed;
    public float minXDist;
    float t;

    //public bool rand = false;
    public bool go = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //float dist = Vector3.Distance(gameObject.transform.position, waypoint[num].transform.position);
        
        float Zdist = Mathf.Abs(gameObject.transform.position.z - waypoint[num].transform.position.z);
        float Xdist = Mathf.Abs(gameObject.transform.position.x - waypoint[num].transform.position.x);
        if (go)
        {
            if (Zdist > minZDist)
            {
                MoveZ();
                if (num + 1 == waypoint.Length)
                {
                    num = 0;
                    //go = false;
                }
                else
                {
                    num++;
                }
            }
            else if (Xdist > minXDist)
            {
                MoveX();
                if (num + 1 == waypoint.Length)
                {
                    num = 0;
                     go = false;
                }
                else
                {
                    num++;
                }

            }
          
        }
        
    }
    public void MoveZ()
    {
        //gameObject.transform.LookAt(waypoint[num].transform.position);
        //gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.SmoothStep(transform.position.z, waypoint[num].transform.position.z, ((Time.time - Time.deltaTime) / speed)));
    }

    public void MoveX()
    {
        //gameObject.transform.LookAt(waypoint[num].transform.position);
        //gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        //transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, waypoint[num].transform.position.x, ((Time.time - Time.deltaTime) / speed)), transform.position.y, transform.position.z);
        
    }
}
