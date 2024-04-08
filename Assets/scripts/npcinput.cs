using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcinput : MonoBehaviour
{
    // Start is called before the first frame update

    public bool space = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true)
            space = true;
                }
}
    