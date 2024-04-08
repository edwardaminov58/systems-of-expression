using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnonsteps : MonoBehaviour
{
    public GameObject ultimaMain;
    public ultimaplayercontrol other;
    // Start is called before the first frame update

    public void turnOn()
    {
        other.moving = false;
        other.GetComponent<SpriteRenderer>().enabled = true;
        ultimaMain.GetComponent<BoxCollider2D>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
