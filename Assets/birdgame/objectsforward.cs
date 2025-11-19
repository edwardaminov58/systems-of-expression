using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsforward : MonoBehaviour
{
    Vector3 forwardmovement;
    public float constantForward;
    public GameObject ground;
    public float groundoffset;
    // Start is called before the first frame update
    void Start()
    {
        forwardmovement = new Vector3(0, 0, -1);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, ground.transform.position.y + groundoffset, transform.position.z) + (forwardmovement * constantForward * Time.deltaTime); 

    }
}
