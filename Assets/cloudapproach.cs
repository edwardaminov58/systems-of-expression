using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class cloudapproach : MonoBehaviour
{
    public GameObject bird;
    public GameObject cloudCollide;

    public Color setColorStart;
    public Color setColorEnd;
    public float returnDistance;
    bool enter = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //float distance = Vector3.Distance(transform.position, bird.transform.position);
       
            //float t = endDistance / distance;
           
            //Debug.Log(gameObject.name + "colorDistance = " + t);
            //Debug.Log("transform.position = " + transform.position);
            
 
            //cloudCollide.GetComponent<Image>().color = Color.Lerp(setColorStart, setColorEnd, t);



            //    if (distance < .1f)
            //    {
            //        enter = true;
            //    }

            //}
            //if (enter == true)
            //{
            //    float n = returnDistance / distance;
            //    cloudCollide.GetComponent<Image>().color = Color.Lerp(setColorEnd, setColorStart, n);
            //    if (distance > returnDistance)
            //        enter = false;
        

    }

}
