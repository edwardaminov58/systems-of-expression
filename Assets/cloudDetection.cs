using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class cloudDetection : MonoBehaviour
{
    float t;
    float[] Distances;
    public float startDistance;
    public float endDistance;
    float lowestDistance = Mathf.Infinity;
    Collider[] hitcolliders;
    LayerMask singleCloud;
    public GameObject cloudCollide;
    bool entered = false;
    // Start is called before the first frame update
    void Start()
    {
        singleCloud = LayerMask.GetMask("singleCloud");
        
    }

    // Update is called once per frame
    void Update()
    {

        hitcolliders = (Physics.OverlapSphere(transform.position, startDistance, singleCloud));
        if (hitcolliders.Length > 0)
        {
            entered = true;
            foreach (var hitcollider in hitcolliders)
            {
                Debug.Log("cloud!" + hitcollider.transform.position);
                float distance = Vector3.Distance(hitcollider.ClosestPoint(transform.position), transform.position);
                Debug.Log("distance=" + distance);
                t = endDistance / distance;
                Debug.Log("cloud t =" + t);

                // else
                // cloudCollide.GetComponent<Image>().color = new Color(cloudCollide.GetComponent<Image>().color.r, cloudCollide.GetComponent<Image>().color.g, cloudCollide.GetComponent<Image>().color.b, 0);
                if (t < 1f)
                {

                    //lowestDistance = distance;
                    Debug.Log("newLow!" + lowestDistance);

                    // colorLerp(hitcollider.GetComponent<cloudapproach>().setColorStart, hitcollider.GetComponent<cloudapproach>().setColorEnd);

                    cloudCollide.GetComponent<Image>().color = Color.Lerp(hitcollider.GetComponent<cloudapproach>().setColorStart, hitcollider.GetComponent<cloudapproach>().setColorEnd, t);
                }

            }



        }
        //else if (entered == true)
        //{
        //    float t = Time.deltaTime;
        //    cloudCollide.GetComponent<Image>().color = Color.Lerp(cloudCollide.GetComponent<Image>().color, new Color(cloudCollide.GetComponent<Image>().color.r, cloudCollide.GetComponent<Image>().color.g, cloudCollide.GetComponent<Image>().color.b, 0), t);
        //}

    //void colorLerp(Color a, Color b)
    //{

    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "singleCloud")
    //    {
    //        Debug.Log("cloudTrigger");
    //        float t = 100 / Vector3.Distance(other.transform.position, transform.position);
    //        cloudCollide.GetComponent<Image>().color = Color.Lerp(other.GetComponent<cloudapproach>().setColorStart, other.GetComponent<cloudapproach>().setColorEnd, t);

    //    }
}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "singleCloud")
    //    {
    //        float t = startDistance / Vector3.Distance(other.transform.position, transform.position);
    //        cloudCollide.GetComponent<Image>().color = Color.Lerp(other.GetComponent<cloudapproach>().setColorEnd, other.GetComponent<cloudapproach>().setColorStart, t);
    //    }
    //}
}
