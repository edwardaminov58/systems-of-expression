using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flight : MonoBehaviour
{
    //public Vector2 limits = new Vector2(5, 3);
    public Vector2 maxlimit;
    public Vector2 minlimit;
    float horizontal;
    float vertical;
    float x;
    float y;
    float TiltL;
    float TiltR;
    public float turnSpeed;
    float initialSpeed;
    public float leanSpeed;
    //private float activeSpeed;
    public float acceleration;
    public Rigidbody rb;
    public float burst;
    public float burstspeed;
    Animator anim;
    public float altitudeMax;
    public float altitudeMin;
    public float noseMax;
    public float nosediveSpeed;
    public float damage;
    public float constantDrop;
    public float constantForward;
    public float FspeedMin;
    public float FspeedMax;
    //float currentForwardSpeed;
    public float speedup;
    public float slowdownMin;
    public float slowdownMax;
    public float speedupMin;
    public float speedupMax;
    public float speedtime;
    public float speedtimeMax;
    public float slowspeed;
    public float cooldown;
    float startBurst;
    float startSpeedupMax;
    float startSlowdownMax;
    //float threshhold = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        //currentForwardSpeed = constantForward;
        //rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        initialSpeed = turnSpeed;
        startBurst = burst;
        startSpeedupMax = speedupMax;
        startSlowdownMax = slowdownMax;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + .5f, FspeedMin, FspeedMax);
        Forward();
        //Debug.Log(currentForwardSpeed);
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical") * -1;
        //if (horizontal < -threshhold) x = -1;
        //else if (horizontal > threshhold) x = 1;
        //else x = 0;
        //activeSpeed = Mathf.Lerp(activeSpeed, Input.GetAxisRaw("Horizontal") * turnSpeed, acceleration * Time.deltaTime);

        Steer(x, y, turnSpeed);
        if ((y < 0) && (TiltL < 0.1) && (TiltR < 0.1))
            NoseDive(x, y, nosediveSpeed);
        else
            anim.SetBool("nosedive", false);
        //HorizontalLean(transform, x, 60, .0f);
        anim.SetFloat("turningValue", x);
        //Debug.Log(x);
        //transform.position += transform.right * activeSpeed * Time.deltaTime;
        if ((Input.GetButton("Jump")) && (anim.GetBool("flapbool") == false) && (TiltL < 0.1) && (TiltR < 0.1) && (anim.GetBool("damage") == false))
        {
            Flap(burst, burstspeed);
            
        }

        if ((Input.GetButtonDown("Speed")) && (anim.GetBool("speed") == false) && (anim.GetBool("slow") == false))
        {
            StartCoroutine(Speed());
        }
        if ((Input.GetButton("Slow")) && (anim.GetBool("slow") == false) && (anim.GetBool("speed") == false))
        {
            StartCoroutine(Slow());
        }
        //if (Input.GetButtonDown("Speed"))
        //    anim.SetBool("speed", true);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, constantForward);

        ClampPosition();
        //rb.velocity = new Vector3(x * turnSpeed, Mathf.Clamp(rb.velocity.y - constantDrop, altitudeMin, altitudeMax), rb.velocity.z);
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y - constantDrop, altitudeMin, altitudeMax), rb.velocity.z);
        TiltL = Input.GetAxis("Left Tilt");
        TiltR = Input.GetAxis("Right Tilt");
        anim.SetFloat("TurnL", TiltL);
        anim.SetFloat("TurnR", TiltR);
        //Debug.Log(TiltL);
        if (TiltL > 0 && x < -0.1)
        {
            turnSpeed = leanSpeed;

        }
        else if (TiltR > 0 && x > 0.1)
        {
            turnSpeed = leanSpeed;
        }
        else
        {
            turnSpeed = initialSpeed;
        }
    }

    void Forward()
    {
        transform.position += new Vector3(0, 0, 1) * constantForward * Time.deltaTime;
    }
    void Steer(float x, float y, float speed)
    {
        ClampPosition();
        transform.localPosition += new Vector3(x, 0, 0) * speed * Time.deltaTime;
        //anim.SetFloat("Velocity", y);
        //if (x < 0)
        //{
        //    anim.SetBool("turning", true);
        //}
        //else if (x > .1f)
        //    anim.SetBool("turning", true);
        //else
        //    anim.SetBool("turning", false);

    }
    //void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    //{
    //    Vector3 targetEulerAngels = target.localEulerAngles;
    //    target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));

    //}


    void NoseDive(float x, float y, float speed)
    {
        anim.SetBool("nosedive", true);
        //transform.localPosition += new Vector3(0, y, 0) * speed * Time.deltaTime;
        rb.AddForce(0, y * speed, 0, ForceMode.VelocityChange);
        //rb.velocity = new Vector3(rb.velocity.x, -speed, rb.velocity.z);
        


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
        //transform.localPosition += new Vector3(0, burst, 0);
        //change this transform
        rb.AddForce(0, burst, 0, ForceMode.VelocityChange);
        anim.SetBool("flapbool", true);
        ReduceStrength();
        //rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(burst, altitudeMin, altitudeMax), rb.velocity.z);

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
        //anim.SetBool("turnleft", false);
        anim.SetBool("damage", false);

    }
    //void endSpeed()
    //{
    //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    //    anim.SetBool("speed", false);
    //}
    private void OnCollisionEnter(Collision collision)
    {

        rb.AddForce(0, -damage, 0, ForceMode.VelocityChange);
        Debug.Log("drop");
        float n = Random.Range(-1f, 1f);
        anim.SetBool("damage", true);
        anim.SetFloat("damagefloat", n);



        //constantForward = 100f;
        //StopCoroutine(Speed());
    }

    void ReduceStrength()
    {
        burst = Mathf.Clamp(burst-5, 10, startBurst);
        speedupMax = Mathf.Clamp(speedupMax-25, speedupMin, speedupMax);
        slowdownMax = Mathf.Clamp(slowdownMax + 25, slowdownMax, speedupMin);
       
    }
    void RestoreStrength()
    {
        burst = startBurst;
        speedupMax = startSpeedupMax;
        slowdownMax = startSlowdownMax;

    }

    void reverseAnimation()
    {
        anim.speed = -.25f;
    }

    IEnumerator Speed()
    {
        //for (float speed = speedup; speed < speedupMax; speed++)
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedup);
        //    speed++;
        //    speedup = speed;
        //}
        //if (constantForward < speedupMax-1f)
        //{
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedup);
        anim.SetBool("speed", true);
        for (float t = 0f; t < 1f; t += Time.deltaTime / speedtime)
        {
            constantForward = Mathf.SmoothStep(constantForward, speedupMax, t);
            Debug.Log(constantForward);
            yield return null;
        }
        yield return new WaitForSeconds(speedtime);
        for (float t = 0f; t < 1f; t += Time.deltaTime / speedtime)
        {
            constantForward = Mathf.SmoothStep(constantForward, speedupMin, t);
            Debug.Log(constantForward);
            yield return null;
        }
       // yield return new WaitForSeconds(cooldown);
        anim.SetBool("speed", false);
        ReduceStrength();
        yield break;
        //}


        //yield return new WaitForSeconds(.5f);
        ////for (float speedup = 10; speedup > speedupMin; speedup++)
        ////{
        ////    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedup);
        ////    speedup--;
        ////}
        //do
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedup);
        //    speedup = speedup - 10;
        //    yield return null;
        //}
        //while ((speedup >= speedupMin) && (anim.GetBool("damage") == false));
        //speedup = speedupMin;
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        //StopAllCoroutines();


    }

    IEnumerator Slow()
    {
        anim.SetBool("slow", true);
        for (float t = 0f; t < 1f; t += Time.deltaTime / slowspeed)
        {
            constantForward = Mathf.SmoothStep(constantForward, slowdownMax, t);
            Debug.Log(constantForward);
            yield return null;
        }
        yield return new WaitForSeconds(speedtime);
        for (float t = 0f; t < 1f; t += Time.deltaTime / slowspeed)
        {
            constantForward = Mathf.SmoothStep(constantForward, speedupMin, t);
            Debug.Log(constantForward);
            yield return null;
        }
        //yield return new WaitForSeconds(cooldown);
        anim.SetBool("slow", false);
        ReduceStrength();
        yield break;
    }
       // void SpeedUp()
        //{
        //rb.AddForce(0, 0, Mathf.Clamp(burst, 0, 50),  ForceMode.VelocityChange);

        //    //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Clamp(rb.velocity.z + speedup, speedupMin, speedupMax));
        //    //Debug.Log(rb.velocity.z);
        //    //if (rb.velocity.z == speedupMax)
        //    //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, constantForward);
        //    //if (anim.GetBool("speed") == true)
        //    //{
        //        //speedtime = speedtime - 1f;
        //        //Debug.Log(speedtime);

        //        if (speedup <= speedupMax) {
        //            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedup);
        //            speedup++;
        //                }
        //        else if (speedup >= speedupMin)
        //        {
        //            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedup);
        //            speedup--;
        //            anim.SetBool("speed", false);
        //        }

        //        //rb.AddForce(0, 0, speedup, ForceMode.VelocityChange);
        //    //}



    //}
    }
