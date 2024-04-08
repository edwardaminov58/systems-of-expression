using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map2 : MonoBehaviour
{
    
    //public bool map = false;
    public ultimaplayercontrol other;
    public GameObject theMap;
    //public GameObject character;
    private float stepNeeded;
    public turnoffmovement turner;
    // Start is called before the first frame update
    void Start()
    {
        stepNeeded = Random.Range(8, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (other.steps >= stepNeeded)
        {
            print("encounter");
            turner.turnOff();
            theMap.SetActive(true);
            Destroy(this);
            
        }
            
    }
}
