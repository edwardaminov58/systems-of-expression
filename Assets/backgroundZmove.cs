using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundZmove : MonoBehaviour
{
    public GameObject bird;
    public float Zoffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Zoffset = Zoffset / bird.transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, bird.transform.position.z + Zoffset - (bird.transform.position.y * 4.5f));
    }
}
