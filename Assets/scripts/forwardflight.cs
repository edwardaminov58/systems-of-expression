using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardflight : MonoBehaviour
{
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        
    }
}


