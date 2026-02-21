using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preyspawner : MonoBehaviour
{
    public GameObject player;
    public GameObject mouse;
    bool startSpawning = true;
    public float spawnHeight;
    public float spawnRate;
    public float spawnStart;
    public float leftLimit;
    public float rightLimit;
    public float playerLead;
    //public Vector2 maxlimit;
    //public Vector2 minlimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z + playerLead);
        //ClampPosition();
        if (player.transform.position.y < spawnHeight)
        {

            if (startSpawning == false)
            {
                InvokeRepeating("Spawn", spawnStart, spawnRate);
                startSpawning = true;
            }

        }
        else
        {
            startSpawning = false;
            CancelInvoke();
        }
    }

    void Spawn()
    {
        Instantiate(mouse, new Vector3(Random.Range(leftLimit, rightLimit), transform.position.y, transform.position.z), transform.rotation);

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
