using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waypoint1_2 : MonoBehaviour
{
    // Use this for initialization
    public GameObject door;
    Animator animator;
    //bool opendoor;
    public Quaternion currentRotation;
    public Vector3 currentPosition;
    public bool moving = false;
    public float ForwardDist;
    public float RightDist;
    public float LeftDist;
    public float UpDist;
    public float DownDist;
    public float FinalForwardDist;
    public bool center = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;
    public bool left = false;
    bool downok = false;
    public bool bottommost = false;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public GameObject room5;
    public GameObject leftarrow;
    public GameObject rightarrow;
    public GameObject uparrow;
    public GameObject downarrow;




    public Coroutine CurrentAct;

    void Start()
    {
        animator = door.GetComponent<Animator>();

        DoThing(moveForward(5f));
       // StartCoroutine(arrows());
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x < -11 && transform.position.x > -12) && (transform.position.y < 12 && transform.position.y > 11))
        {
            left = false;
            down = false;
            right = false;
            up = false;
            center = true;
            //print(transform.position.x);
        }
        else if (transform.position.x < -1 && transform.position.x > -2)
        {
            left = true;
            up = false;
            down = false;
            center = false;
            right = false;
            //print(transform.position.x);
        }
        else if (transform.position.x < -21 && transform.position.x > -22)
        {
            left = false;
            up = false;
            down = false;
            center = false;
            right = true;
            //print(transform.position.x);

        }
        else if (transform.position.y < 22 && transform.position.y > 21)
        {
            left = false;
            up = true;
            down = false;
            center = false;
            right = false;
            //print(transform.position.y);
        }
        else if (transform.position.y < 2 && transform.position.y > 1)
        {
            left = false;
            up = false;
            down = true;
            center = false;
            right = false;
            //print(transform.position.y);
        }
        else if (transform.position.y < -8 && transform.position.y > -9)
        {
            left = false;
            up = false;
            down = false;
            center = false;
            right = false;
            bottommost = true;

        }
        if (bottommost == true)
            DoThing(moveFinalForward(5f));

        if (CurrentAct != null)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.D) && ((center == true) || (left == true))) || (Input.GetKeyDown(KeyCode.RightArrow) && ((center == true) || (left == true))))
        {
            ////moving = true;
            //print("turnedright");
            ////StartCoroutine(RotateCamera(new Vector3 (0, currentRotation.y + 90, 0), .2f));
            //DoThing(RotateCamera(new Vector3(0, currentRotation.y + 90, 0), .18f));
            ////currentRotation, currentRotation.y + 90, Time.deltaTime);
            print("movedright");
            DoThing(moveRight(2f));
            downok = true;


        }
        else if ((Input.GetKeyDown(KeyCode.A) && ((center == true) || (right == true))) || (Input.GetKeyDown(KeyCode.LeftArrow) && ((center == true) || (right == true))))
        {
            //moving = true;
            //print("turnedleft");
            //DoThing(RotateCamera(new Vector3(0, currentRotation.y - 90, 0), .18f));
            print("movedright");
            DoThing(moveLeft(2f));
            downok = true;

        }
        else if ((Input.GetKeyDown(KeyCode.W) && (center == true)) || (Input.GetKeyDown(KeyCode.UpArrow) && (center == true)))
        {
            //moving = true;
            print("movedup");
            //RaycastHit Hit;
            // if (Physics.Raycast(transform.position, transform.forward, out Hit, 5f))
            // {
            //print("Wall!");
            //StartCoroutine (headDonk);
            // }
            DoThing(moveUp(2f));
            downok = true;
        }
        else if ((downok == true) && ((Input.GetKey(KeyCode.S) && ((center == true) || (up == true) || (down == true))) || (Input.GetKeyDown(KeyCode.DownArrow) && ((center == true) || (up == true) || (down == true)))))
        {
            //moving = true;
            //print("turnedaround");
            //DoThing(RotateCamera(new Vector3(0, currentRotation.y + 180, 0), .3f));
            print("moveddown");
            DoThing(moveDown(2f));

        }

    }

    public void DoThing(IEnumerator act)
    {
        leftarrow.SetActive(false);
        rightarrow.SetActive(false);
        uparrow.SetActive(false);
        downarrow.SetActive(false);

        if (CurrentAct != null)
        {
            Debug.Log("ERROR: You started a new action before the old one was done.");
            return;
        }
        CurrentAct = StartCoroutine(doThing(act));
       
        StartCoroutine(arrows());
    }

    IEnumerator doThing(IEnumerator act)
    {

        yield return StartCoroutine(act);
        CurrentAct = null;
        //StopCoroutine (CurrentAct);

    }

    IEnumerator RotateCamera(Vector3 byAngles, float inTime)
    {
        //moving = true;7
        Quaternion currentRotation = transform.rotation;
        //currentPosition.y += 0.1f;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, toAngle, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1,
            Mathf.SmoothStep(0, 1, t))));

            yield return null;

        }
        transform.rotation = toAngle;
        moving = false;
    }
    // IEnumerator moveForward(float inTime){
    //// Vector3 currentPosition = transform.position;
    //// Vector3 toPosition = currentPosition + byFeet;
    //// for (float t = 0f; t < 1f; t += Time.deltaTime / inTime) {
    //// transform.position = Vector3.Lerp (currentPosition, toPosition , Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
    //// yield return null;
    // transform.position += transform.forward * Time.deltaTime/ inTime;
    // //}
    // moving = false;
    // yield return null;
    // }
    IEnumerator moveForward(float inTime)
    {
        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition + transform.forward * ForwardDist;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        moving = false;
        room1.SetActive(true);
        room2.SetActive(true);
        room3.SetActive(true);
        room4.SetActive(true);
        room5.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(arrows());

    }
    IEnumerator moveRight(float inTime)
    {

        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition + transform.right * RightDist;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, t));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        moving = false;
    }
    IEnumerator moveUp(float inTime)
    {

        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition + transform.up * UpDist;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, t));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        moving = false;
    }
    IEnumerator moveLeft(float inTime)
    {

        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition - transform.right * LeftDist;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, t));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        moving = false;
    }
    IEnumerator moveDown(float inTime)
    {

        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition - transform.up * DownDist;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, t));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        moving = false;
    }
    IEnumerator moveFinalForward(float inTime)
    {

        animator.SetBool("opendoor", true);
        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition + transform.forward * FinalForwardDist;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        StartCoroutine(loadAsync());
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        moving = false;

        //opendoor = true;


    }
    IEnumerator loadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("train scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    IEnumerator arrows()
    {
        yield return new WaitForSecondsRealtime(4);
        if ((center == true)&&(CurrentAct == null)&&(downok !=true))
        {
            //yield return new WaitForSecondsRealtime(2);
            CenterArrow();
        }
        else if ((right == true)&&(CurrentAct == null))
        {
            RightArrow();
        }
        else if((left==true)&&(CurrentAct==null))
        {
           LeftArrow();
        }
        else if((up == true)&&(CurrentAct == null))
        {
            UpArrow();
        }
        else if ((center == true) && (CurrentAct == null) && (downok == true))
        {
            Downarrow();
        }
        //yield return new WaitForSecondsRealtime(2);
        //if ((left == true) && (CurrentAct == null))
        //{
        //    yield return new WaitForSecondsRealtime(3);
        //    //leftarrow.SetActive(true);
        //    if (CurrentAct != null)
        //        yield break;
        //    leftarrow.SetActive(false);
        //    rightarrow.SetActive(true);
        //    uparrow.SetActive(false);
        //    downarrow.SetActive(false);
        //}

            //else if ((right == true) && (CurrentAct == null))
            //{
            //    if (CurrentAct != null)
            //        yield break;
            //    yield return new WaitForSecondsRealtime(3);
            //    if (CurrentAct != null)
            //        yield break;
            //    leftarrow.SetActive(true);
            //    rightarrow.SetActive(false);
            //    uparrow.SetActive(false);
            //    downarrow.SetActive(false);

            //}
            //else if ((up == true) && (CurrentAct == null))
            //{
            //    if (CurrentAct != null)
            //        yield break;
            //    yield return new WaitForSecondsRealtime(3);
            //    if (CurrentAct != null)
            //        yield break;
            //    rightarrow.SetActive(false);
            //    uparrow.SetActive(false);
            //    downarrow.SetActive(true);
            //    leftarrow.SetActive(false);

            //}
            //else if ((downok == true) && (center == true) && (CurrentAct == null))
            //{
            //    if (CurrentAct != null)
            //        yield break;
            //    yield return new WaitForSecondsRealtime(3);
            //    if (CurrentAct != null)
            //        yield break;
            //    leftarrow.SetActive(true);
            //    rightarrow.SetActive(true);
            //    uparrow.SetActive(true);
            //    downarrow.SetActive(true);

            //}
            //else
            //{

            //    //yield return new WaitForSecondsRealtime(3);
            //    if (center == true)
            //    {

            //        yield return new WaitForSecondsRealtime(2);
            //        if (CurrentAct != null)
            //            yield break;
            //        leftarrow.SetActive(true);
            //        rightarrow.SetActive(true);
            //        uparrow.SetActive(true);
            //        //downarrow.SetActive(false);
            //    }
            //}

            //yield return new WaitForSecondsRealtime(1);


    }

    void CenterArrow()
    {
        leftarrow.SetActive(true);
        rightarrow.SetActive(true);
        uparrow.SetActive(true);
        downarrow.SetActive(false);
    }
    void UpArrow()
    {
        leftarrow.SetActive(false);
        rightarrow.SetActive(false);
        uparrow.SetActive(false);
        downarrow.SetActive(true);
    }
    void LeftArrow()
    {
        leftarrow.SetActive(false);
        rightarrow.SetActive(true);
        uparrow.SetActive(false);
        downarrow.SetActive(false);
    }
    void RightArrow()
    {
        leftarrow.SetActive(true);
        rightarrow.SetActive(false);
        uparrow.SetActive(false);
        downarrow.SetActive(false);
    }
    void Downarrow()
    {
        leftarrow.SetActive(true);
        rightarrow.SetActive(true);
        uparrow.SetActive(true);
        downarrow.SetActive(true);
    }
    //IEnumerator headDonk;
}
  