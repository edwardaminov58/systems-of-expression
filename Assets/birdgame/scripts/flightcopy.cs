using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class flightcopy : MonoBehaviour
{
    public float minForward;
    public float maxForward;
    public float minLens;
    public float maxLens;
    public float occlusionPlaneStart;
    public float occlusionPlaneEnd;
    private bool noWindA = false;
    private bool wind = false;
    //public Vector2 limits = new Vector2(5, 3);
    public Vector2 maxlimit;
    public Vector2 minlimit;
    //float horizontal;
    //float vertical;
    //float xprime;
    float startTurnSpeed;
    float x;
    float y;
    float TiltL;
    float TiltR;
    public float turnSpeed;
    float initialSpeed;
    public float leanSpeed;
    //private float activeSpeed;
    //public float acceleration;
    public Rigidbody rb;
    public float burst;
    public float angle;
    Animator anim;
    public float Gravity;
    public float maxSoar;
    public float noseMax;
    public float nosediveSpeed;
    public float damage;
    public float preyBounce = 30;
    public float dropFromRise;
    public float constantForward;
    //public float FspeedMin;
    //public float FspeedMax;
    ////float currentForwardSpeed;
    public float speedup;
    public float slowdownMin;
    public float slowdownMax;
    public float speedupMin;
    public float speedupMax;
    //public float speedtime;
    //public float speedtimeMax;
    //public float slowTime;
    public float cooldown;
    float startBurst;
    float startSpeedupMax;
    float startSlowdownMin;
    public float startNoseSpeed;
    public CinemachineVirtualCamera vcam;
    public float cameraDamp;
    float startCameraDamp;
    float windstrength;
    float complexDrop;
    float startlens;
    public float lensThreshold;
    public float farPlaneThreshold;
    public float reduceFlap = 5;
    public float FlapMin = 10;
    public float sideBurstTime;
    public float rideSpeed;
    bool bounced = false;
    public float bounceBoostSpeed = 20;
    float sideBurstEnd;
    float originalSpeed;
    public float speedReset;
    public float speedtimeStart;
    public float speedtimeDuration;
    public float speedtimeStop;
    public float slowtimeStart;
    public float slowtimeDuration;
    public float slowtimeStop;
    float flapDrop = 1;
    float dropFromRiseStart;
    public float brakePull;




    Vector3 forwardmovement;
    //float threshhold = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        dropFromRiseStart = dropFromRise;
        originalSpeed = constantForward;
        startCameraDamp = cameraDamp;
        //currentForwardSpeed = constantForward;
        //rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        initialSpeed = turnSpeed;
        startBurst = burst;
        startSpeedupMax = speedupMax;
        startSlowdownMin = slowdownMin;
        forwardmovement = new Vector3(0, 0, 1);
        //complexDrop = Mathf.Log(Mathf.Pow(constantDrop, 3) + 5) * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //flapDropTime();
        //Debug.Log("Velocity: " + rb.velocity);
        //Debug.Log(sideBurstEnd);
        //Occlusion();
        FOVchange();
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
        if (x == 0)
            startTurnSpeed = 0;
        if ((y < 0))
        {
            anim.SetFloat("nosedivevalue", y);
            if (bounced != true)
            {
                NoseDive(x, y, nosediveSpeed);
                if (x != 0)
                {
                    //Debug.Log("dive");
                    anim.SetBool("diving", true);
                }
            }
        }
        else
        {
            nosediveSpeed = startNoseSpeed;
            anim.SetBool("nosedive", false);
            anim.SetBool("diving", false);
            // vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView + .15f, 25, 60f); 
        }
        //HorizontalLean(transform, x, 60, .0f);
        anim.SetFloat("turningValue", x);
        //Debug.Log(x);
        //transform.position += transform.right * activeSpeed * Time.deltaTime;
        if ((Input.GetButton("Jump")) && (anim.GetBool("flapbool") == false) && (anim.GetBool("damage") == false))
        {
            Flap(burst, angle);

        }

        if ((Input.GetButton("Speed")) && (anim.GetBool("speed") == false) && (anim.GetBool("slow") == false))
        {
            StartCoroutine(Speed());
        }
        if ((Input.GetButton("Slow")) && (anim.GetBool("slow") == false) && (anim.GetBool("speed") == false))
        {
            StartCoroutine(Slow());
        }
        if (wind == true)
        {

            rb.AddForce(0, .5f, 0, ForceMode.VelocityChange);
            if (y > 0.1)
            {
                windRide();
            }


        }
        else
        {
            anim.SetBool("windride", false);
            if ((y > 0) && (noWindA == false))
            {

                //StartCoroutine(noWind());
            }

        }

        //if (Input.GetButtonDown("Speed"))
        //    anim.SetBool("speed", true);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, constantForward);

        ClampPosition();
        rb.velocity = new Vector3((x * turnSpeed + sideBurstEnd), Mathf.Clamp(rb.velocity.y - dropFromRise, Gravity, maxSoar), rb.velocity.z);
        //Debug.Log("velocity:" + rb.velocity.y);
        //Debug.Log(rb.velocity);
        // rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y - constantDrop, altitudeMin, altitudeMax), rb.velocity.z);
        TiltL = Input.GetAxis("Left Tilt");
        TiltR = Input.GetAxis("Right Tilt");
        anim.SetFloat("TurnL", TiltL);
        anim.SetFloat("TurnR", TiltR);
        //Debug.Log(complexDrop);
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
    void windRide()
    {
        anim.SetBool("windtouch", false);
        anim.SetBool("windride", true);
        float t = Time.deltaTime * rideSpeed;
        rb.AddForce(0, Mathf.SmoothStep(rb.velocity.y, windstrength, t), 0, ForceMode.VelocityChange);
    }
    void FOVchange()
    {
        //vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView, 35, 60);
        float t = gameObject.transform.localPosition.y / lensThreshold;
        //vcam.m_Lens.FieldOfView = Mathf.Lerp(60, 35, t);
        //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.Lerp(6, 20, t);

        vcam.m_Lens.FieldOfView = Mathf.Lerp(minLens, maxLens, t);
        //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 13;

    }
    void Occlusion()
    {
        float t = gameObject.transform.localPosition.y / farPlaneThreshold;
        vcam.m_Lens.FarClipPlane = Mathf.Lerp(occlusionPlaneStart, occlusionPlaneEnd, t);
    }
    void Forward()
    {

        transform.localPosition += forwardmovement * constantForward * Time.deltaTime;
        //if (TiltL > 0)
        //{
        //    Quaternion deltarotation = Quaternion.Euler(new Vector3(0, -20, 0) * Time.deltaTime);
        //    // Quaternion.LookRotation(new Vector3(0, 0, -1));
        //    //transform.localRotation = Quaternion.LookRotation(forwardmovement);
        //    rb.MoveRotation(rb.rotation * deltarotation);
        //    forwardmovement = Quaternion.Euler(new Vector3(0, -20, 0) * Time.deltaTime) * forwardmovement;

        //}
        //if (TiltR > 0)
        //{
        //    Quaternion deltarotation = Quaternion.Euler(new Vector3(0, 20, 0) * Time.deltaTime);
        //    // Quaternion.LookRotation(new Vector3(0, 0, -1));
        //    //transform.localRotation = Quaternion.LookRotation(forwardmovement);
        //    rb.MoveRotation(rb.rotation * deltarotation);
        //    forwardmovement = Quaternion.Euler(new Vector3(0, 20, 0) * Time.deltaTime) * forwardmovement;

        //}
    }
    void Steer(float x, float y, float speed)
    {
        ClampPosition();
        //transform.localPosition += new Vector3(x, 0, 0) * speed * Time.deltaTime;
        //sideBurst = x * burst;
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

    IEnumerator noWind()
    {
        noWindA = true;
        anim.SetBool("falseshift", true);
        for (float t = 0f; t < 1f; t += Time.deltaTime / .3f)
        {
            //vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(0, cameraDamp, t);
            transform.localPosition += new Vector3(0, Mathf.SmoothStep(0, .1f, t), 0);
            yield return null;
        }
        yield return new WaitForSeconds(.3f);
        for (float t = 0f; t < 1f; t += Time.deltaTime / .3f)
        {
            //vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(0, cameraDamp, t);
            transform.localPosition -= new Vector3(0, Mathf.SmoothStep(0, .1f, t), 0);
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        noWindA = false;
        anim.SetBool("falseshift", true);
        //yield break;
    }
    void NoseDive(float x, float y, float speed)
    {

        anim.SetBool("nosedive", true);
        //transform.localPosition += new Vector3(0, y, 0) * speed * Time.deltaTime;
        if (transform.position.y <= (minlimit.y + 1))
        {
            nosediveSpeed = bounceBoostSpeed;
            //Mathf.Clamp(nosediveSpeed++, startNoseSpeed, bounceBoostSpeed);
            Debug.Log(bounceBoostSpeed);
        }
        else
            Mathf.Clamp(nosediveSpeed++, 0, 100);
        rb.AddForce(0, y * nosediveSpeed, 0, ForceMode.VelocityChange);
        //rb.velocity = new Vector3(rb.velocity.x, -speed, rb.velocity.z);
        //vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView-.15f, 50, 60);
        //anim.SetBool("diving", true);


    }
    void ClampPosition()
    {
        Vector3 localPos = transform.localPosition;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        // transform.position = Camera.main.ViewportToWorldPoint(pos);
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, minlimit.x, maxlimit.x), Mathf.Clamp(localPos.y, minlimit.y, maxlimit.y), localPos.z);
        //Debug.Log("constant drop: " + constantDrop);
    }

    void flapDropTime()
    {
        //if (anim.GetBool("flapbool") == true)
        //{
        //    constantDrop = flapDrop;

        //}
        //else
        //    constantDrop = constantdropStart;
    }
    void Flap(float burst, float angle)
    {
        //transform.localPosition += new Vector3(0, burst, 0);
        
        //rb.AddForce((x * burst), burst, 0, ForceMode.VelocityChange); 
        float t = 0;
        //Debug.Log(t);
        if (TiltL > 0.5f)
        {


            StartCoroutine(horizontalFlap(-3));
            rb.AddForce(0, burst / 3, 0, ForceMode.VelocityChange);

           
        }
        else if (TiltR > 0.5f)
        {

            StartCoroutine(horizontalFlap(3));
            rb.AddForce(0, burst / 3, 0, ForceMode.VelocityChange);
        }
        else if (TiltL > 0.1f)
        {

            StartCoroutine(horizontalFlap(-3));
            rb.AddForce(0, burst / 1.5f, 0, ForceMode.VelocityChange);
        }
        else if (TiltR > 0.1f)
        {

            StartCoroutine(horizontalFlap(3));
            rb.AddForce(0, burst / 1.5f, 0, ForceMode.VelocityChange);
        }
        else if (x > 0.1f)
        {
            

            StartCoroutine(horizontalFlap(5));
            rb.AddForce(0, burst, 0, ForceMode.VelocityChange);
        }
        else if (x < -.1f)
        {
            

            StartCoroutine(horizontalFlap(-5));
            rb.AddForce(0, burst, 0, ForceMode.VelocityChange);
        }
        else
            rb.AddForce(0, burst, 0, ForceMode.VelocityChange);
        anim.SetBool("flapbool", true);
        t = 0;
        //sideBurstEnd = 0;
        //Debug.Log(t);
        ReduceStrength();
        //vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView + 10, 25, 75);
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
        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);


    }
    //void endSpeed()
    //{
    //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    //    anim.SetBool("speed", false);
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "prey")
        {
            Destroy(collision.gameObject);
            RestoreStrength();
            StartCoroutine(PreyBounce());

        }
        else
        {
            rb.AddForce(0, -damage, 0, ForceMode.VelocityChange);
            //Debug.Log("drop");
            float n = Random.Range(-1f, 1f);
            anim.SetBool("damage", true);
            //anim.SetFloat("damagefloat", n);
        }




        //constantForward = 100f;
        //StopCoroutine(Speed());
    }
    IEnumerator PreyBounce()
    {
        bounced = true;
        //float t = 0;
        //t += Time.deltaTime * nosediveSpeed;
        //rb.velocity = new Vector3(rb.velocity.x, Mathf.Lerp(0, nosediveSpeed + preyBounce, t), rb.velocity.z);
        rb.AddForce(0, Mathf.Abs(nosediveSpeed + preyBounce), 0, ForceMode.VelocityChange);
        yield return new WaitForSeconds(.5f);
        bounced = false;
        nosediveSpeed = startNoseSpeed;
        


    }

    //IEnumerator bounceRecovery()
    //{
    //    float t = 0;


    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "wind")
    //    {
    //        Debug.Log("windy");
    //        anim.SetBool("windtouch", true);
    //        rb.AddForce(0, 1.05f, 0, ForceMode.VelocityChange);
    //        if (y > 0)
    //        {
    //            rb.AddForce(0, y * 1f, 0, ForceMode.VelocityChange);
    //        }

    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wind")
        {
            anim.SetBool("windtouch", true);
            wind = true;
            //Debug.Log("windy");
           // windstrength = other.gameObject.GetComponent<wind>().windstrength;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wind")
        {
            wind = false;
            //Debug.Log("no wind");
            anim.SetBool("windtouch", false);

        }
        //if (other.gameObject.tag == "wind")
        //{
        //    Debug.Log("no wind");
        //    anim.SetBool("windtouch", false);


        //}
    }
    //void nowind() 
    //{   if ()
    //    rb.AddForce(0, y * 1f, 0, ForceMode.VelocityChange);
    //}

    void ReduceStrength()
    {
        burst = Mathf.Clamp(burst - reduceFlap, FlapMin, startBurst);
        speedupMax = Mathf.Clamp(speedupMax - 25, speedupMin, speedupMax);
        slowdownMax = Mathf.Clamp(slowdownMin + 5, slowdownMin, slowdownMax);
        cameraDamp = Mathf.Clamp(cameraDamp - .05f, 0.01f, .25f);

    }
    void RestoreStrength()
    {
        burst = startBurst;
        speedupMax = startSpeedupMax;
        slowdownMin = startSlowdownMin;
        cameraDamp = startCameraDamp;

    }

    void reverseAnimation()
    {
        anim.speed = -.25f;
    }
    IEnumerator horizontalFlap(float n)
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / sideBurstTime)
        {
          
      
            sideBurstEnd = Mathf.Lerp(burst/n, 0, t);


            yield return null;


        }
        sideBurstEnd = 0;
        ;

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
        float previousSpeed = constantForward;
        float newSpeed = Mathf.Clamp(previousSpeed + 10, minForward, maxForward);
        //vcam.m_Lens.FieldOfView = 70;
        for (float t = 0f; t < 1f; t += Time.deltaTime / speedtimeStart)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(0, cameraDamp, t);
            
            constantForward = Mathf.SmoothStep(constantForward, speedupMax, t);
            //Debug.Log(constantForward);
            yield return null;
        }

        yield return new WaitForSeconds(speedtimeDuration);
        anim.SetBool("speed", false);
        for (float t = 0f; t < 1f; t += Time.deltaTime / speedtimeStop)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(cameraDamp, 0, t);
            constantForward = Mathf.SmoothStep(speedupMax, newSpeed,  t);
           // Debug.Log(constantForward);
            yield return null;
        }
        
        //yield return new WaitForSeconds(speedReset);
        //constantForward = originalSpeed;
        // yield return new WaitForSeconds(cooldown);
        //yield return new WaitForSeconds(.1f);

        //vcam.m_Lens.FieldOfView = 60;
        
        
        ReduceStrength();
        
        
        //yield break;
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
        float previousSpeed = constantForward;
        anim.SetBool("slow", true);
        for (float t = 0f; t < 1f; t += Time.deltaTime / slowtimeStart)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.SmoothStep(13, brakePull, t);
            //Debug.Log(constantForward);
            yield return null;
        }
       
        yield return new WaitForSeconds(slowtimeDuration);
        for (float t = 0f; t < 1f; t += Time.deltaTime / slowtimeStop)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.SmoothStep(brakePull, 13, t);
            constantForward = Mathf.SmoothStep(slowdownMin, Mathf.Clamp(previousSpeed - 10, minForward, maxForward), t);
            //Debug.Log(constantForward);
            yield return null;
        }
        anim.SetBool("slow", false);
        //yield return new WaitForSeconds(speedReset);
        //constantForward = originalSpeed;
        //yield return new WaitForSeconds(cooldown);


        ReduceStrength();
        
        
        
        //yield break;
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
   public void SpeedChange()
    {
        StartCoroutine(speedChange());    
    }
   IEnumerator speedChange()
    {
        
        for (float t = 0f; t < 1f; t += Time.deltaTime / speedtimeStart)
        {
            //vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(0, cameraDamp, t);

            constantForward = Mathf.SmoothStep(constantForward, 1, t);
            //Debug.Log(constantForward);
            yield return null;
        }
    }
}
