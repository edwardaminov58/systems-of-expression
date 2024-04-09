using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flight : MonoBehaviour
{
    //public Vector2 limits = new Vector2(5, 3);
    public Vector2 maxlimit = new Vector2(5, 3);
    public Vector2 minlimit = new Vector2(-6, -8);
    float x;
    float y;
    public float turnSpeed;
    private float activeSpeed;
    public float acceleration;
    public Rigidbody rb;
    public float burst;
    public float burstspeed;
    Animator anim;
    public float altitudeMax;
    public float altitudeMin;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical") * -1;
        //activeSpeed = Mathf.Lerp(activeSpeed, Input.GetAxisRaw("Horizontal") * turnSpeed, acceleration * Time.deltaTime);

        Steer(x, y, turnSpeed);
        if (y < 0)
            NoseDive(x, y, turnSpeed);
        else
            anim.SetBool("nosedive", false);
        //HorizontalLean(transform, x, 30, .05f);
        transform.position += transform.right * activeSpeed * Time.deltaTime;
        if ((Input.GetButton("Jump")) && (anim.GetBool("flapbool") == false))
        {
            Flap(burst, burstspeed);
        }

        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, altitudeMin, altitudeMax), rb.velocity.z);
        

    }
    void Steer(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, 0, 0) * speed * Time.deltaTime;
        if (x < 0)
            anim.SetBool("turnleft", true);
        else
            anim.SetBool("turnleft", false);
        ClampPosition();
        if (x > .1f)
            anim.SetBool("turnright", true);
        else
            anim.SetBool("turnright", false);

    }
    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }


    void NoseDive(float x, float y, float speed)
    {
        anim.SetBool("nosedive", true);
        //transform.localPosition += new Vector3(0, y, 0) * speed * Time.deltaTime;
        rb.AddForce(0, y * speed, 0);


    }
    void ClampPosition()
    {
        Vector3 localPos = transform.localPosition;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        // transform.position = Camera.main.ViewportToWorldPoint(pos);
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, minlimit.x, maxlimit.x), Mathf.Clamp(localPos.y, minlimit.y, maxlimit.y), localPos.z);
    }
    void Flap(float burst, float speed)
    {
        //transform.localPosition += new Vector3(0, speed, 0) * burst * Time.deltaTime;
        //change this transform
        rb.AddForce(0, burst, 0 );
        anim.SetBool("flapbool", true);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minlimit.x, minlimit.y, transform.position.z), new Vector3(maxlimit.x, minlimit.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(maxlimit.x, minlimit.y, transform.position.z), new Vector3(maxlimit.x, maxlimit.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(maxlimit.x, maxlimit.y, transform.position.z), new Vector3(minlimit.x, maxlimit.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(minlimit.x, maxlimit.y, transform.position.z), new Vector3(minlimit.x, minlimit.y, transform.position.z));
    }

    void endFlap()
    {

        anim.SetBool("flapbool", false);
        anim.SetBool("turnleft", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(0, -burst, 0);
        Debug.Log("drop");
    }

    void reverseAnimation()
    {
        anim.speed = -.25f;
    }
}
