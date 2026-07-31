using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class flight : MonoBehaviour
{
    public float cameraTiltThreshold;
    float turnspeed;
    public CameraProfile cameraProfile;
    public GameObject bird;
    float maxForwardBoost;
    float loseForwardBoost;
    float windRise;
    public float ExitSpeed;
    float windtime = 0;
    public flightData FlightData;
    //public float minForward;
    //public float maxForward;
    //public Material birdmat;
    public float occlusionPlaneStart;
    public float occlusionPlaneEnd;
    private bool noWindA = false;
    private bool wind = false;
    //public Vector2 limits = new Vector2(5, 3);
    //public Vector2 maxlimit;
    //public Vector2 minlimit;
    //float horizontal;
    //float vertical;
    //float xprime;
    float startTurnSpeed;
    float x;
    float y;
    public float TiltL;
    public float TiltR;
    //public float turnSpeed;
    float initialSpeed;
    //public float leanSpeed;
    //private float activeSpeed;
    //public float acceleration;
    public Rigidbody rb;
    public float burst;
    public float angle;
    public Animator anim;
    //public float Gravity;
    float maxSoar;
    public float noseMax;
    float nosediveSpeed;
    public float damage;
    public float preyBounce = 30;
    //public float dropFromRise;
    public float constantForward;
    //public float FspeedMin;
    //public float FspeedMax;
    ////float currentForwardSpeed;
    public float speedup;
    //public float slowdownMin;
    //public float slowdownMax;
    //public float speedupMin;
    //public float speedupMax;
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
    Vector3 windstrength;
    Vector3 windMax;
    float complexDrop;
    float startlens;
    public float lensThreshold;
    public float farPlaneThreshold;
    public float reduceFlap = 5;
    public float FlapMin = 10;
    public float sideBurstTime;
    float rideSpeed;
    bool bounced = false;
    public float bounceBoostSpeed = 20;
    float sideBurstEnd;
    float originalSpeed;
    public float speedReset;
    //public float speedtimeStart;
    //public float speedtimeDuration;
    //public float speedtimeStop;
    //public float slowtimeStart;
    //public float slowtimeDuration;
    //public float slowtimeStop;
    float flapDrop = 1;
    float dropFromRiseStart;
    public float brakePull;
    //public bool SimControls;
    public altitudeManager AltitudeManager;



    Vector3 forwardmovement;
    //float threshhold = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        //currentForwardSpeed = constantForward;
        //rb = GetComponent<Rigidbody>();
        //anim = gameObject.GetComponent<Animator>();
        //birdmat = gameObject.GetComponent<SpriteRenderer>().material;
        forwardmovement = new Vector3(0, 0, 1);
        //complexDrop = Mathf.Log(Mathf.Pow(constantDrop, 3) + 5) * Time.deltaTime;
    }

    public void Initialize()
    {
        dropFromRiseStart = FlightData.dropFromRise;
        originalSpeed = constantForward;
        startCameraDamp = cameraDamp;
        initialSpeed = FlightData.turnSpeed;
        startBurst = burst;
        startSpeedupMax = FlightData.speedupMax;
        startSlowdownMin = FlightData.slowdownMin;
    }

    public void changeData(flightData newData)
    {
        
        FlightData = newData;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Transparency();
        Debug.Log("velocity = " + rb.velocity);
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

        Steer(x, y, FlightData.turnSpeed);
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
            nosediveSpeed = cameraProfile.nosediveSpeed;
            anim.SetBool("nosedive", false);
            anim.SetBool("diving", false);
            // vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView + .15f, 25, 60f); 
        }
        //HorizontalLean(transform, x, 60, .0f);
        //cameraTiltChange();
        //normal
        anim.SetFloat("turningValue", x);

        //simControl
        //if (TiltL > 0.1)
        //    anim.SetFloat("turningValue", TiltL * -1);
        //if (TiltR > 0.1)
        //    anim.SetFloat("turningvalue", TiltR);

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
            Debug.Log("wind");
            rb.AddForce(0, .5f, 0, ForceMode.VelocityChange);
            if (y > 0.1)
            {

                windRide();
            }
            else
            {
                windtime = 0;

            }

        }
        else
        {
            maxSoar = FlightData.maxSoar;
            loseForwardBoost = FlightData.loseForwardBoost;
            maxForwardBoost = FlightData.maxForwardBoost;
            anim.SetBool("windride", false);
            windtime = 0;
            if ((y > 0) && (noWindA == false))
            {

                //StartCoroutine(noWind());
            }

        }

        //if (Input.GetButtonDown("Speed"))
        //    anim.SetBool("speed", true);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, constantForward);

        ClampPosition();

        //normal
        rb.velocity = new Vector3((x * turnspeed + sideBurstEnd), Mathf.Clamp(rb.velocity.y - FlightData.dropFromRise, FlightData.Gravity, maxSoar), Mathf.Clamp(rb.velocity.z - loseForwardBoost, 0, maxForwardBoost));

        //simControl
        //rb.velocity = new Vector3((((TiltL * -1) + TiltR) * FlightData.turnSpeed + sideBurstEnd), Mathf.Clamp(rb.velocity.y - FlightData.dropFromRise, FlightData.Gravity, maxSoar), Mathf.Clamp(rb.velocity.z - loseForwardBoost, 0, maxForwardBoost));


        //Debug.Log("velocity:" + rb.velocity.y);
        //Debug.Log(rb.velocity);
        // rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y - constantDrop, altitudeMin, altitudeMax), rb.velocity.z);
        TiltL = Input.GetAxis("Left Tilt");
        TiltR = Input.GetAxis("Right Tilt");

        //normal
        anim.SetFloat("TurnL", TiltL);
        anim.SetFloat("TurnR", TiltR);

        //simControl
        //if (x != 0)
        //{
        //    if (x < 0)
        //    {
        //        anim.SetFloat("TurnL", x * -1);
        //    }
        //    if (x > 0)
        //    {
        //        anim.SetFloat("TurnR", x);
        //    }
        //}

        //Debug.Log(complexDrop);
        //Debug.Log(TiltL);

        //normal
        if (TiltL > 0 && x < -0.1)
        {
            turnspeed = FlightData.leanSpeed;

        }
        else if (TiltL > 0 && x < -0.1)
        {
            turnspeed = FlightData.leanReverseSpeed;
        }
        else if (TiltR > 0 && x > 0.1)
        {
            turnspeed = FlightData.leanSpeed;
        }
        else if (TiltR > 0 && x < 0.1)
        {
            turnspeed = FlightData.leanReverseSpeed;
        }
        else
        {
            turnspeed = FlightData.turnSpeed;
        }  
    }
    void windRide()
    {
        Quaternion rotation = Quaternion.LookRotation(bird.transform.localPosition, bird.transform.localPosition);
        Vector3 normalizedWindStrength = rotation * windstrength; 
        Debug.Log("normalized: " + normalizedWindStrength);
        Debug.Log("normalizedRotation: " + rotation);
        
        anim.SetBool("windtouch", false);
        anim.SetBool("windride", true);
        windtime += Time.deltaTime / rideSpeed;
        float t = windtime;
        //windRise = Mathf.SmoothStep(0, windMax.y, t)
        //Debug.Log("t= " + t);
      //  Debug.Log("windstrength: " + windstrength);
        Debug.Log("time.deltatime: " + Time.deltaTime);

        //Debug.Log("windstrength= " + windstrength);
        rb.AddForce(Mathf.SmoothStep(0, windstrength.x, t), Mathf.SmoothStep(0, Mathf.Abs(normalizedWindStrength.y), t), Mathf.SmoothStep(0, windstrength.z, t), ForceMode.VelocityChange);
        //rb.AddForce(Vector3.Lerp(new Vector3(0, 0, 0), windstrength, t), ForceMode.VelocityChange);
        //rb.AddForce(0, Mathf.SmoothStep(0, windstrength, t), 0, ForceMode.VelocityChange);
    }
    void FOVchange()
    {
        //vcam.m_Lens.FieldOfView = Mathf.Clamp(vcam.m_Lens.FieldOfView, 35, 60);
        float t = gameObject.transform.localPosition.y / lensThreshold;
        //vcam.m_Lens.FieldOfView = Mathf.Lerp(60, 35, t);
        //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.Lerp(6, 20, t);

        vcam.m_Lens.FieldOfView = Mathf.Lerp(cameraProfile.minLens, cameraProfile.maxLens, t);
        //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraProfile.CameraDistance;

    }
    void cameraTiltChange()
    {
        //if (cameraProfile.third == true)
       // {
            //float altStart1 = AltitudeManager.altitudes[1];
            //float altStart2 = AltitudeManager.altitudes[2];
            float altStart = AltitudeManager.altitudes[AltitudeManager.currentHeightLayer];

            //Debug.Log("camera t=" + t);
            Debug.Log("layer =" + AltitudeManager.currentHeightLayer);
            // Debug.Log("alt start = " + altStart);
            Debug.Log("threshhold = " + cameraTiltThreshold);
            // if (bird.transform.position.y < AltitudeManager.altitudes[1])
            //   vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0f;
            //if (bird.transform.position.y >= AltitudeManager.altitudes[1] && bird.transform.position.y <= AltitudeManager.altitudes[2] - 75) {
            //    float t = (gameObject.transform.localPosition.y - altStart1) / cameraTiltThreshold;
            //    vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = Mathf.Lerp(0, 15f, t);
            //    Debug.Log("lerp1");
            //        }
            //else if (bird.transform.position.y >= AltitudeManager.altitudes[2] - 25) {
            //    float t = (gameObject.transform.localPosition.y - altStart2) / cameraTiltThreshold;
            //    vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = Mathf.Lerp(15, 0, t);
            //    Debug.Log("lerp2"); }
            if (AltitudeManager.currentHeightLayer == 0)
            {
                //vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0f;
                vcam.transform.rotation = Quaternion.Euler(0, vcam.transform.rotation.y, vcam.transform.rotation.z);
            }
            else if (AltitudeManager.currentHeightLayer == 1)
            {
                float t = (gameObject.transform.localPosition.y - altStart) / cameraTiltThreshold;
                vcam.transform.rotation = Quaternion.Euler(Mathf.Lerp(0, 15, t), vcam.transform.rotation.y, vcam.transform.rotation.z);
                //vcam.transform.rotation = Quaternion.Lerp(vcam.transform.rotation, Quaternion.Euler(15, vcam.transform.rotation.y, vcam.transform.rotation.z), t);
                //vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = Mathf.Lerp(0, 15f, t);

            }
            else if (AltitudeManager.currentHeightLayer == 2)
            {
                float t = (gameObject.transform.localPosition.y - altStart) / cameraTiltThreshold;
                vcam.transform.rotation = Quaternion.Euler(Mathf.Lerp(15, 0, t), vcam.transform.rotation.y, vcam.transform.rotation.z);
                //vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = Mathf.Lerp(15, -2.5f, t);
            }
       // }

    }
    void Occlusion()
    {
        float t = gameObject.transform.localPosition.y / farPlaneThreshold;
        vcam.m_Lens.FarClipPlane = Mathf.Lerp(occlusionPlaneStart, occlusionPlaneEnd, t);
    }
    void Forward()
    {

        transform.localPosition += forwardmovement * constantForward * Time.deltaTime;

        //mode7turningControl
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
        if (transform.position.y <= (FlightData.minlimit.y + 1))
        {
            nosediveSpeed = bounceBoostSpeed;
            //Mathf.Clamp(nosediveSpeed++, startNoseSpeed, bounceBoostSpeed);
            //Debug.Log(bounceBoostSpeed);
        }
        else
            Mathf.Clamp(nosediveSpeed++, 0, 100);
        rb.AddForce(0, y * nosediveSpeed, cameraProfile.nosediveFMultiplier*nosediveSpeed, ForceMode.VelocityChange);
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
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, FlightData.minlimit.x, FlightData.maxlimit.x), Mathf.Clamp(localPos.y, FlightData.minlimit.y, FlightData.maxlimit.y), localPos.z);
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
        Gizmos.DrawLine(new Vector3(FlightData.minlimit.x, FlightData.minlimit.y, transform.position.z), new Vector3(FlightData.maxlimit.x, FlightData.minlimit.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(FlightData.maxlimit.x, FlightData.minlimit.y, transform.position.z), new Vector3(FlightData.maxlimit.x, FlightData.maxlimit.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(FlightData.maxlimit.x, FlightData.maxlimit.y, transform.position.z), new Vector3(FlightData.minlimit.x, FlightData.maxlimit.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(FlightData.minlimit.x, FlightData.maxlimit.y, transform.position.z), new Vector3(FlightData.minlimit.x, FlightData.minlimit.y, transform.position.z));
    }

    void endFlap()
    {
        rb = GameObject.FindGameObjectWithTag("flight").GetComponent<Rigidbody>();
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
            constantForward -= 2f;
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
        nosediveSpeed = cameraProfile.nosediveSpeed;
        


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
            windstrength = other.gameObject.GetComponent<wind>().windstrength;
            maxSoar = other.gameObject.GetComponent<wind>().maxSoar;
            rideSpeed = other.gameObject.GetComponent<wind>().rideSpeed;
            loseForwardBoost = other.gameObject.GetComponent<wind>().loseForwardBoost;
            maxForwardBoost = other.gameObject.GetComponent<wind>().maxForwardBoost;

        }
    }

    IEnumerator ExitWind()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime / rideSpeed)
        {
            Debug.Log(t);
            ///rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y,Mathf.SmoothStep(rb.velocity.z, 0, rb.velocity.z));
            //rb.AddForce(Mathf.SmoothStep(0, windstrength.x, t), Mathf.SmoothStep(0, windstrength.y, t), Mathf.SmoothStep(0, windstrength.z, t), ForceMode.VelocityChange);
            // rb.AddForce(-windstrength.x, -windstrength.y, -windstrength.z);
            yield return null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wind")
        {
            wind = false;
            windtime = 0;
            
            //windstrength = new Vector3(0, 0, 0);
            //Debug.Log("no wind");
            anim.SetBool("windtouch", false);
            //StartCoroutine(ExitWind());
            
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
        burst = Mathf.Clamp(burst - (burst/reduceFlap), FlapMin, startBurst);
        FlightData.speedupMax = Mathf.Clamp(FlightData.speedupMax - 25, FlightData.speedupMin, FlightData.speedupMax);
        FlightData.slowdownMax = Mathf.Clamp(FlightData.slowdownMin + 5, FlightData.slowdownMin, FlightData.slowdownMax);
        cameraDamp = Mathf.Clamp(cameraDamp - .05f, 0.01f, .25f);

    }
    void RestoreStrength()
    {
        burst = startBurst;
        FlightData.speedupMax = startSpeedupMax;
        FlightData.slowdownMin = startSlowdownMin;
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
          
      
            sideBurstEnd = Mathf.Lerp(burst /n, 0, t);


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
        float newSpeed = Mathf.Clamp(previousSpeed + 10, FlightData.minForward, FlightData.maxForward);
        //vcam.m_Lens.FieldOfView = 70;
        for (float t = 0f; t < 1f; t += Time.deltaTime / FlightData.speedtimeStart)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(0, cameraDamp, t);
            
            constantForward = Mathf.SmoothStep(constantForward, FlightData.speedupMax, t);
            //Debug.Log(constantForward);
            yield return null;
        }

        yield return new WaitForSeconds(FlightData.speedtimeDuration);
        anim.SetBool("speed", false);
        for (float t = 0f; t < 1f; t += Time.deltaTime / FlightData.speedtimeStop)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(cameraDamp, 0, t);
            constantForward = Mathf.SmoothStep(FlightData.speedupMax, newSpeed,  t);
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
        for (float t = 0f; t < 1f; t += Time.deltaTime / FlightData.slowtimeStart)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.SmoothStep(13, brakePull, t);
            //Debug.Log(constantForward);
            yield return null;
        }
       
        yield return new WaitForSeconds(FlightData.slowtimeDuration);
        for (float t = 0f; t < 1f; t += Time.deltaTime / FlightData.slowtimeStop)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Mathf.SmoothStep(brakePull, 13, t);
            constantForward = Mathf.SmoothStep(FlightData.slowdownMin, Mathf.Clamp(previousSpeed - 10, FlightData.minForward, FlightData.maxForward), t);
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
        
        for (float t = 0f; t < 1f; t += Time.deltaTime / FlightData.speedtimeStart)
        {
            //vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = Mathf.SmoothStep(0, cameraDamp, t);

            constantForward = Mathf.SmoothStep(constantForward, 1, t);
            //Debug.Log(constantForward);
            yield return null;
        }
    }
}
