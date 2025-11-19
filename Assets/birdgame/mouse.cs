using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    //public preyspawner Preyspawner;
    // Start is called before the first frame update
    int n;
    public float speed;
    private float startX;
    public float leftLimit;
    public float rightLimit;
    float leftVector;
    float rightVector;
    SpriteRenderer sr;
    public float hideHeight;
    float cameraHeight;

    void Start()
    {
        //n =  Random.Range(1, 3);
        startX = transform.localPosition.x;
        sr = GetComponent<SpriteRenderer>();
        if (startX <= 0)
        {
            sr.flipX = true;

        }
        leftVector = transform.localPosition.x + Random.Range(leftLimit, -5);
        rightVector = transform.localPosition.x + Random.Range(5, rightLimit);
        Debug.Log("leftVector "+ leftVector + " rightVector " + rightVector);
        //Debug.Log("leftlimit " + leftLimit + " rightlimit " + rightLimit);

    }

    // Update is called once per frame 
    private void LateUpdate()
    {
        cameraHeight = Camera.main.transform.position.y;
        
        if (cameraHeight > hideHeight)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    void FixedUpdate()
    {


        if (startX <= 0)
        {
            moveRight();
            
        }
        else
        {
            moveLeft();
        }


        //    if (transform.position.x < leftLimit)
        //    {
        //        transform.position += new Vector3(-1 * speed, 0, 0);
        //    }
        //    else if (transform.position.x >= leftLimit)
        //    {
        //        transform.position += new Vector3(1 * speed, 0, 0);
        //    }
        //}
        //else if (startX >= 0.1f)
        //{
        //    if (transform.position.x < rightLimit)
        //    {
        //        transform.position += new Vector3(1 * speed, 0, 0);
        //    }
        //    else if (transform.position.x >= rightLimit)
        //    {
        //        transform.position += new Vector3(-1 * speed, 0, 0);
        //    }
        //}
        //else if (n == 2)
        //    transform.localPosition += new Vector3(-1 * speed, 0, 0);
        //turn around code

        //else if (transform.localPosition.x > Preyspawner.rightLimit)
        //{
        //    transform.localPosition += new Vector3(-1 * speed, 0, 0);
        //}
        //else if (transform.localPosition.x > 0)
        //{
        //    if (transform.localPosition.x > Preyspawner.rightLimit)
        //    {
        //        transform.localPosition += new Vector3(-1 * speed, 0, 0);
        //    }
        //    else
        //    {
        //        transform.localPosition += new Vector3(1 * speed, 0, 0);
        //    }
        //}


    }
    void moveLeft()
    {
        transform.localPosition += Vector3.left * speed;
        if ((transform.localPosition.x < leftVector) || (transform.localPosition.x > rightVector)) { 
             turnAround();
        }



    }
    void moveRight()
    {
        transform.localPosition += Vector3.right * speed;
        if ((transform.localPosition.x < leftVector) || (transform.localPosition.x > rightVector))
        {
            turnAround();
        }
    }
    void turnAround()
    {
        speed = speed * -1;
        if (sr.flipX == true)
            sr.flipX = false;
        else
            sr.flipX = true;
        return;

        
    }
}
