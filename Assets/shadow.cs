using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    public GameObject player;
    float n;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        n = player.transform.position.y/2;
        transform.position = new Vector3(player.transform.position.x, -2f, player.transform.position.z + n);
    }
}
