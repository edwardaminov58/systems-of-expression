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

    void Start()
    {
        //n =  Random.Range(1, 3);
        startX = transform.position.x;

    }

    // Update is called once per frame 
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
        transform.position += Vector3.left * speed;
        if ((transform.position.x < leftLimit) || (transform.position.x > rightLimit))
             turnAround();

        

    }
    void moveRight()
    {
        transform.position += Vector3.right * speed;
        if ((transform.position.x < leftLimit) || (transform.position.x > rightLimit))
            turnAround();
    }
    void turnAround()
    {
        speed = speed * -1;
    }
}
