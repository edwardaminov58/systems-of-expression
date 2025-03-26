using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preyspawner : MonoBehaviour
{
    public GameObject player;
    public GameObject mouse;
    bool startSpawning;
    //public Vector2 maxlimit;
    //public Vector2 minlimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, -26, transform.position.z);
        //ClampPosition();
        if (player.transform.position.y < -5f)
        {

            if (startSpawning == false)
            {
                InvokeRepeating("Spawn", 0, .25f);
                startSpawning = true;
            }
        }
        else
            startSpawning = false;
    }
    void Spawn()
    {
        Instantiate(mouse, new Vector3(Random.Range(-25, 25), transform.position.y, transform.position.z), transform.rotation);

    }
    //void ClampPosition()
    //{
    //    Vector3 localPos = transform.localPosition;
    //    Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
    //    pos.x = Mathf.Clamp01(pos.x);
    //    pos.y = Mathf.Clamp01(pos.y);
    //    // transform.position = Camera.main.ViewportToWorldPoint(pos);
    //    transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, minlimit.x, maxlimit.x), Mathf.Clamp(localPos.y, minlimit.y, maxlimit.y), localPos.z);
    //}
}
