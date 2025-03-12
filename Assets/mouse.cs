using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    // Start is called before the first frame update
    int n;
    void Start()
    {
      n =  Random.Range(1, 2);
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        if (n == 1)
            transform.localPosition += new Vector3(1, 0, 0);
        else if (n == 2)
            transform.localPosition += new Vector3(-1, 0, 0);
    }
}
