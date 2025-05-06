using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // gameObject.GetComponent<billboard>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

    }
}
