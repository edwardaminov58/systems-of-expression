using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardflight : MonoBehaviour
{
    public float speed;
    public float startSpeed;
    public float gainSpeed;
    public float MaxSpeed;
    public float MinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        speed = Mathf.Clamp(speed + .5f, MinSpeed, MaxSpeed);
        transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        
    }
}


