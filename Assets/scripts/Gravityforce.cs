using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravityforce : MonoBehaviour
{
    public float gravity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -1, 0) * gravity * Time.deltaTime;
    }
}
