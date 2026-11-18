using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class cloudDetection : MonoBehaviour
{
    float[] Distances;
    public float startDistance;
    public float endDistance;
    float lowestDistance = Mathf.Infinity;
    Collider[] hitcolliders;
    LayerMask singleCloud;
    public GameObject cloudCollide;
    // Start is called before the first frame update
    void Start()
    {
        singleCloud = LayerMask.GetMask("singleCloud");
        
    }

    // Update is called once per frame
    void Update()
    {
        hitcolliders = (Physics.OverlapSphere(transform.position, startDistance, singleCloud));
        if (hitcolliders != null)
        {
            foreach (var hitcollider in hitcolliders)
            {
                Debug.Log("cloud!" + hitcollider.transform.position);
                float distance = Vector3.Distance(hitcollider.transform.position, transform.position);
                Debug.Log("distance=" + distance);
                float t = endDistance / distance;
                Debug.Log("cloud t =" + t);
                if (t<2f)
                {
                  
                    //lowestDistance = distance;
                    Debug.Log("newLow!" + lowestDistance);

                   // colorLerp(hitcollider.GetComponent<cloudapproach>().setColorStart, hitcollider.GetComponent<cloudapproach>().setColorEnd);
                    
                    cloudCollide.GetComponent<Image>().color = Color.Lerp(hitcollider.GetComponent<cloudapproach>().setColorStart, hitcollider.GetComponent<cloudapproach>().setColorEnd, t);
                }
               // else
                  // cloudCollide.GetComponent<Image>().color = new Color(cloudCollide.GetComponent<Image>().color.r, cloudCollide.GetComponent<Image>().color.g, cloudCollide.GetComponent<Image>().color.b, 0);
            }
        }
        }
       
    }

    //void colorLerp(Color a, Color b)
    //{

    //}
//}
