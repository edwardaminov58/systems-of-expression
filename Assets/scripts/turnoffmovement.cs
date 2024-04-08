using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnoffmovement : MonoBehaviour
{
    public GameObject ultimaMain;
    public ultimaplayercontrol other;
    // Start is called before the first frame update
    public  void turnOff()
    {
        other.moving = true;
        other.GetComponent<SpriteRenderer>().enabled = false;
        ultimaMain.GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
