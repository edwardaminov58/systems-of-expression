using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcetest : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetButtonDown("Jump"))){
            rb.AddForce(new Vector3(1, 1, 1), ForceMode.VelocityChange);
        }
    }
}
