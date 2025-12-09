using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public float[] distance;
    //public float distance1;
    //public float distance2;
    //public float distance3;
    //public float distance4;
    public Sprite[] sprites;
    float distanceFromCamera;
    float distanceFromBird;
    [SerializeField] Sprite currentSprite;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.GetComponent<billboard>().enabled = false;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        Vector3 heading = transform.position - Camera.main.transform.position;
        distanceFromCamera = Vector3.Dot(heading, Camera.main.transform.forward);
        //Debug.Log(distanceFromCamera);
        


        if (distanceFromCamera < .5)
            Destroy(this.gameObject);

        else
            for (int n = 0; n < distance.Length; n++)
            {
                if (animator == null)
                {
                    if (distanceFromCamera > distance[n])
                    {

                        currentSprite = sprites[n];
                        GetComponent<SpriteRenderer>().sprite = currentSprite;
                        break;
                    }
                }
                else

                {
                    if (distanceFromCamera > distance[n])
                    {

                        animator.SetInteger("n", n);
                        break;
                    }
                }
                
            }
            
    }
}
