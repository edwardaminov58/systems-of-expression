using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnoff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject light;
    void turnOff()
    {
        gameObject.SetActive(false);
    }

    void SetLight()
    {
        light.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
