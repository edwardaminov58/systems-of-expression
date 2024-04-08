using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer2d : MonoBehaviour
{
    // Start is called before the first frame update

    //Transform playerTransform;
    public Rigidbody2D target;
    public int numFrames = 10;
    movement2d player;
    public Queue<Vector2> targetmovement;
    Animator animator;
    private void Start()
    {
        targetmovement = new Queue<Vector2>();
        player = target.GetComponent<movement2d>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {

        if ((player.movement != Vector2.zero) && (player.rbMoving == false))
        {
            
            targetmovement.Enqueue(target.transform.position);
           

        }

        if (targetmovement.Count > numFrames)
        {
            animator.SetFloat("horizontal", target.transform.position.x - transform.position.x);
            animator.SetFloat("vertical", target.transform.position.y - transform.position.y);
            transform.position = targetmovement.Dequeue();
            animator.SetFloat("speed", 1);

            
        }
        if (player.movement != Vector2.zero)
        {
            //if (transform.position.x > target.transform.position.x)
            animator.SetFloat("speed", 0);
            animator.SetFloat("lasthorizontal", target.transform.position.x - transform.position.x);
            animator.SetFloat("lastvertical", target.transform.position.y - transform.position.y);


        }
        
    }

}