using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followparty2d : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Animator animator;
    public Vector2 movement;
    public bool canMove = true;

    //public Rigidbody2D target;
    public int numFrames = 10;
    //movement2d player;
    private Queue<Vector2> delayedInput;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        //targetmovement = new Queue<Vector2>();
        //player = target.GetComponent<movement2d>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // if (player.movement != Vector2.zero)
        //targetmovement.Enqueue(target.transform.position);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    private void Update()
    {
        if (canMove == true)
        {
           // if(targetmovement.Count > numFrames) { }
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("speed", movement.sqrMagnitude);

            if ((movement.x != 0) || (movement.y != 0))
            {
                animator.SetFloat("lasthorizontal", movement.x);
                animator.SetFloat("lastvertical", movement.y);
            }
        }
    }

    public void toggleMovement()
    {
        if (canMove == false)
            canMove = true;
        else if (canMove == true)
            canMove = false;
    }
    //private void LateUpdate()
    //{
    //    movement = transform.position;
    //    Vector2 clampedMovement = new Vector2((int)movement.x, (int)movement.y);
    //    if(clampedMovement.magnitude >= 1.0f)
    //    {
    //        movement = movement - clampedMovement;
    //        if(clampedMovement != Vector2.zero)
    //        {
    //           transform.position = 
    //        }
    //    }
    //}
}
