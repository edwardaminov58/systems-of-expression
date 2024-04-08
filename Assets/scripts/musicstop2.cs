using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicstop2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        GameObject.Destroy(music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
