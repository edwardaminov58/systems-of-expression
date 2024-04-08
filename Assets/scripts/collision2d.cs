using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class collision2d : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("triggered");
        Flowchart.BroadcastFungusMessage("run");

    }
}
