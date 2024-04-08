using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject man;
    public GameObject intro;
    public GameObject birdcu;

    void turnOnBirdCU()
    {
        birdcu.SetActive(true);
    }
    void turnOffMan()
    {
        man.SetActive(false);
    }

    void turnOffIntro()
    {
        intro.SetActive(false);
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
