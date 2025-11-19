using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2d : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Animator animator;
    public Vector2 movement;
    public bool canMove= true;
    //Vector2 deltaPosition;
    followplayer2d follower;
    public GameObject targeting;
    public bool rbMoving = false;
    public Vector2 CurrentPosition;
    public Vector2 LastPosition;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        follower = targeting.GetComponent<followplayer2d>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


}

    private void Update()
    {
        if (canMove == true)
        {
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
        CurrentPosition = rb.position;
        if (CurrentPosition == LastPosition)
            rbMoving = true;
        else
            rbMoving = false;
        LastPosition = CurrentPosition;

    }

   public void toggleMovement()
    {
        if (canMove == false)
            canMove = true;
        else if (canMove == true)
        {
            movement.x = 0;
            movement.y = 0;
            animator.SetFloat("speed", 0);
            canMove = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
  
        

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rbMoving = false;
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    canMove = false;
    //    movement.x = 0;
    //    movement.y = 0;
    //    animator.SetFloat("speed", 0);
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    canMove = true;
    //}

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

