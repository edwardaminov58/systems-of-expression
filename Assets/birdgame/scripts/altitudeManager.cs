using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altitudeManager : MonoBehaviour
{
    public GameObject bird;
    public GameObject Base;
    public GameObject Layer1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.transform.position.y < 20)
        {
            Base.SetActive(true);
            Layer1.SetActive(false);
        }
        else if (bird.transform.position.y > 20)
        {
            Base.SetActive(false);
            Layer1.SetActive(true);
        }
    }
}
