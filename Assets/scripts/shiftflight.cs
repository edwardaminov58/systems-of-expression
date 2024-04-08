using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftflight : MonoBehaviour
{

    public float shiftSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Ytranslation = Input.GetAxis("Vertical") * shiftSpeed;
        float Xtranslation = Input.GetAxisRaw("Horizontal") * shiftSpeed;




        Xtranslation *= Time.deltaTime;
        Ytranslation *= Time.deltaTime;


        //if ((transform.position.x < 18f) && (transform.position.x > -18f) && (transform.position.y < 8f) && (transform.position.y > -8f))

        var pos = transform.position;

        transform.Translate(Xtranslation, -Ytranslation, 0);
        pos.x = Mathf.Clamp(transform.position.x, -5, 5);
        pos.y = Mathf.Clamp(transform.position.y, -2f, 2);
        //Vector3 clampedPosition = transform.position;
        //clampedPosition.y = Mathf.Clamp(clampedPosition.y, -7f, -7f);
        // transform.position = clampedPosition;
        transform.position = pos;
    }
}
