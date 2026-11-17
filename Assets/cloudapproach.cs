using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class cloudapproach : MonoBehaviour
{
    public GameObject bird;
    public GameObject cloudCollide;
    public float startDistance;
    public float endDistance;
    public Color setColorStart;
    public Color setColorEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, bird.transform.position);
        if (distance < startDistance)
        { float t = endDistance / distance;
            cloudCollide.GetComponent<Image>().color = Color.Lerp(setColorStart, setColorEnd, t);
            Debug.Log("colorDistance = " + t);
        }
    }
}
