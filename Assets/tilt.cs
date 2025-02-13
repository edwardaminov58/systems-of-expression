using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilt : MonoBehaviour
{
    // Start is called before the first frame update
    float TiltL;
    float TiltR;
    Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TiltL = Input.GetAxisRaw("Left Tilt");
        TiltR = Input.GetAxisRaw("Right Tilt");
        anim.SetFloat("TurnL", TiltL);
        anim.SetFloat("TurnR", TiltR);
    }
}
