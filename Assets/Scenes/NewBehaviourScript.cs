using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstpersonmovement: MonoBehaviour
{
    // Use this for initialization
    public Quaternion currentRotation;
    public Vector3 currentPosition;
    public bool moving = false;

    public Coroutine CurrentAct;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentAct != null)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.D) && moving == false) || (Input.GetKeyDown(KeyCode.RightArrow) && moving == false))
        {
            //moving = true;
            print("turnedright");
            //StartCoroutine(RotateCamera(new Vector3 (0, currentRotation.y + 90, 0), .2f));
            DoThing(RotateCamera(new Vector3(0, currentRotation.y + 90, 0), .18f));
            //currentRotation, currentRotation.y + 90, Time.deltaTime);
        }
        else if ((Input.GetKeyDown(KeyCode.A) && moving == false) || (Input.GetKeyDown(KeyCode.LeftArrow) && moving == false))
        {
            //moving = true;
            print("turnedleft");
            DoThing(RotateCamera(new Vector3(0, currentRotation.y - 90, 0), .18f));
        }
        else if ((Input.GetKeyDown(KeyCode.W) && moving == false) || (Input.GetKeyDown(KeyCode.UpArrow) && moving == false))
        {
            //moving = true;
            print("movedup");
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, transform.forward, out Hit, 5f))
            {
                print("Wall!");
                //StartCoroutine (headDonk);
            }
            else DoThing(moveForward(.18f));
        }
        else if ((Input.GetKey(KeyCode.S) && moving == false) || (Input.GetKey(KeyCode.DownArrow) && moving == false))
        {
            //moving = true;
            print("turnedaround");
            DoThing(RotateCamera(new Vector3(0, currentRotation.y + 180, 0), .3f));

        }

    }

    public void DoThing(IEnumerator act)
    {
        if (CurrentAct != null)
        {
            Debug.Log("ERROR: You started a new action before the old one was done.");
            return;
        }
        CurrentAct = StartCoroutine(doThing(act));
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
        Vector3 toPosition = currentPosition + transform.forward * 10;
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
    }
    //IEnumerator headDonk;



}