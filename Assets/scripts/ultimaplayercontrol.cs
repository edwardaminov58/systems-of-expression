using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultimaplayercontrol : MonoBehaviour
{
    public float steps = 0;
    public float moveSpeed = 1f;
    private float horizontal;
    private float vertical;
    public bool moving = false;
    private Vector2 currentPosition;
    public AudioClip step;
    public AudioClip blocked;
    public Coroutine CurrentAct;
    private AudioSource source;
    public Coroutine CurrentSound;
    public LayerMask selfCollision;

    // Start is called before the first frame update


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {

        if (CurrentAct != null)
        {
            return;
        }

        if ((Input.GetKey(KeyCode.D) && moving == false) || (Input.GetKey(KeyCode.RightArrow) && moving == false))
        {

            print("moveright");
            if (Physics2D.Raycast(transform.position, transform.right, 1f, ~selfCollision))
            {
                print("Wall!");
                //StartCoroutine (headDonk);
                MakeSound(Blocked());

            }
            else  DoThing(moveRight(moveSpeed));

        }
        else if ((Input.GetKey(KeyCode.A) && moving == false) || (Input.GetKey(KeyCode.LeftArrow) && moving == false))
        {
            // moving = true;
            print("moveleft");
            if (Physics2D.Raycast(transform.position, -transform.right, 1f, ~selfCollision))
            {
                print("Wall!");
                //StartCoroutine (headDonk);
                MakeSound(Blocked());

            }
            else DoThing(moveLeft(moveSpeed));
        }
        else if ((Input.GetKey(KeyCode.W) && moving == false) || (Input.GetKey(KeyCode.UpArrow) && moving == false))
        {
            //moving = true;
            print("movedup");
            // RaycastHit Hit;
            if (Physics2D.Raycast(transform.position, transform.up, 1f, ~selfCollision))
            {
                print("Wall!");
                //StartCoroutine (headDonk);
                MakeSound(Blocked());

            }
            else DoThing(moveUp(moveSpeed));
        }
        else if ((Input.GetKey(KeyCode.S) && moving == false) || (Input.GetKey(KeyCode.DownArrow) && moving == false))
        {
            //moving = true;
            print("down");
            if (Physics2D.Raycast(transform.position, -transform.up, 1f, ~selfCollision))
            {
                print("Wall!");
                //StartCoroutine (headDonk);
                MakeSound(Blocked());

            }
            else DoThing(moveDown(moveSpeed));

        }
        void DoThing(IEnumerator act)
        {
            if (CurrentAct != null)
            {
                Debug.Log("ERROR: You started a new action before the old one was done.");
                return;
            }
            CurrentAct = StartCoroutine(doThing(act));
            // moving = true;
            steps++;
        }

        IEnumerator doThing(IEnumerator act)
        {
            yield return StartCoroutine(act);
            CurrentAct = null;
            //StopCoroutine (CurrentAct);
            // gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //yield return new WaitForSeconds(.3f);

        }

    }

    // Update is called once per frame
    IEnumerator moveUp(float inTime)
    {
        Vector2 currentPosition = transform.position;
        Vector2 toPosition = currentPosition + Vector2.up;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector2.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        //moving = false;
        source.clip = step;
        source.Play();
    }
    IEnumerator moveDown(float inTime)
    {
        Vector2 currentPosition = transform.position;
        Vector2 toPosition = currentPosition + Vector2.down;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector2.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        //moving = false;
        source.clip = step;
        source.Play();
    }
    IEnumerator moveRight(float inTime)
    {
        Vector2 currentPosition = transform.position;
        Vector2 toPosition = currentPosition + Vector2.right;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector2.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        //moving = false;
        source.clip = step;
        source.Play();
    }
    IEnumerator moveLeft(float inTime)
    {
        Vector2 currentPosition = transform.position;
        Vector2 toPosition = currentPosition + Vector2.left;
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.position = Vector2.Lerp(currentPosition, toPosition, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, Mathf.SmoothStep(0, 1, t))));
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.position = toPosition;
        //Debug.Log ("MFF: " + transform.position + " / " + currentPosition + " / " + toPosition);
        //moving = false;
        source.clip = step;
        source.Play();
    }
    //IEnumerator headDonk;
    //void turnon()
    //{
    //    moving = false;
    //}

    IEnumerator Blocked()
    {
        source.clip = blocked;
        source.Play();
        // yield break;
        yield return new WaitForSeconds(1f);
    }
    void MakeSound(IEnumerator sound)
    {
        if (CurrentSound != null)
        {
            Debug.Log("ERROR: You started a new action before the old one was done.");
            return;
        }
        CurrentSound = StartCoroutine(makeSound(sound));
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    IEnumerator makeSound(IEnumerator sound)
    {
        yield return StartCoroutine("Blocked");
        CurrentSound = null;
        //StopCoroutine (CurrentAct);
        // gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //yield return new WaitForSeconds(.3f);

    }
}

