using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetsteps : MonoBehaviour
{
    public ultimaplayercontrol other;
    // Start is called before the first frame update
    void Start()
    {
        other.steps = 0;
        //other.moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
