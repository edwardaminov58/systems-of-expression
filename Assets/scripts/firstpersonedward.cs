using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstpersonedward : MonoBehaviour
{

    public Quaternion currentRotation;
    public Vector3 currentPosition;
    public Coroutine CurrentAct;

    void Update()
    {

        if (CurrentAct != null)
        {
            return;
        }

        if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
        {

            print("turnedright");
            DoThing(RotateCamera(new Vector3(0, currentRotation.y + 90, 0), .18f));

        }
        else if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
        {

            print("turnedleft");
            DoThing(RotateCamera(new Vector3(0, currentRotation.y - 90, 0), .18f));
        }
        else if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow)))
        {

            print("movedup");
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, transform.forward, out Hit, 5f))
            {
                print("Wall!");

            }
            else DoThing(moveForward(.18f));
        }
        else if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow)))
        {

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
    }

    IEnumerator RotateCamera(Vector3 byAngles, float inTime)
    {
        Quaternion currentRotation = transform.rotation;

        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, toAngle, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1,
            Mathf.SmoothStep(0, 1, t))));

            yield return null;

        }
        transform.rotation = toAngle;

    }
    IEnumerator moveForward(float inTime)
    {
        Vector3 currentPosition = transform.position;
        Vector3 toPosition = currentPosition + transform.forward * 5;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
    }
}



