using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class windDetection : MonoBehaviour
{
    public float translateSpeed;
    public float windPush;
    public GameObject player;
    public GameObject bird;
    public Rigidbody rb;
    LayerMask wind;
    //Vector3 Detect;
    //Vector3[] Detect;
   // List<Vector3> Detect;
    Collider[] hitcolliders;
    List<GameObject> collisionObjects;
    int index;
    Vector3 Detect;
    Vector3 Distance;
    //var collider;
    // Start is called before the first frame update
    void Start()
    {
        wind = LayerMask.GetMask("wind");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;
        //if (Physics.CheckSphere(player.transform.position, 50f, wind))
        //{


        hitcolliders = (Physics.OverlapSphere(player.transform.position, 50f, wind));
        foreach (var hitcollider in hitcolliders)
        {
            Detect = hitcollider.ClosestPoint(player.transform.position);
            //if (collisionObjects.Contains(hitcollider.gameObject))
            //{
            //    for (int i = 0; i < collisionObjects.Count; i++)
            //    {
            //        Detect[i] = (hitcollider.ClosestPoint(player.transform.position));
            //    }
 
            //}
            //else
            //{
            //    collisionObjects.Add(hitcollider.gameObject);
            //    Detect.Add(hitcollider.ClosestPoint(player.transform.position));
            //}
            //Debug.Log(collisionObjects.Count);
            //Detect = (hitcollider.ClosestPoint(player.transform.position));
        }
        //}
        //else
        //{
        //    //Detect.Clear();
        //    //Detect = Vector3.zero;
        //}
        if (Physics.CheckSphere(player.transform.position, 50f, wind))
        {
            Distance = Detect - new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            if (Distance.magnitude > 0&&Distance.magnitude < 50f)
            {
                //Debug.Log("Detectforce");
                if (Distance.x > 0)
                {
                    float t = translateSpeed / Distance.magnitude ;
                    rb.AddForce(new Vector3((-(1/Distance.magnitude * windPush)), Distance.magnitude * -1, 0));
                    Debug.Log("t = " + t);
                    //bird.transform.Translate(-Distance.magnitude,0, 0 * Time.deltaTime/translateSpeed);
                    Debug.Log("lerp =" + Mathf.Lerp(bird.transform.localPosition.x, -Detect.x, t));
                    bird.transform.position = new Vector3(Mathf.Lerp(player.transform.position.x, -Detect.x, t), bird.transform.position.y, bird.transform.position.z);/* * Time.deltaTime * translateSpeed;*/
                    

                   // bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, Vector3.Lerp(bird.transform.localPosition, -Detect, t), translateSpeed * Time.deltaTime);
                    //bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition,-Detect, translateSpeed * Time.deltaTime);

                }
                else if (Distance.x < 0)
                {
                    float t = 1 / Distance.magnitude * 10;
                    rb.AddForce(new Vector3(((1/Distance.magnitude * windPush)), Distance.magnitude * -1, 0));
                    Debug.Log("t = " + t);
                    bird.transform.Translate(Vector3.Lerp(Vector3.zero, -Detect, t));
                    //bird.transform.Translate(Distance.magnitude, 0, 0 * Time.deltaTime/translateSpeed);
                    //bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, Vector3.Lerp(bird.transform.localPosition, -Detect, t), translateSpeed * Time.deltaTime);
                   // bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, -Detect, translateSpeed * Time.deltaTime);

                }

            }
            Debug.Log("Detect:" + Detect + "Distance: " + Distance + "Distmag: " + Distance.magnitude);
        }
        else
        {
            Detect = (Vector3.zero);
            Debug.Log("DetectZero");
            bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, Vector3.zero, translateSpeed * Time.deltaTime); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bird.transform.position, 50f);
        // if (Physics.CheckSphere(player.transform.position, 50f, wind))
        //{
        //foreach (Vector3 derect in Detect) { 
        //    Gizmos.DrawLine(derect, player.transform.position);
        Gizmos.DrawLine(Detect, bird.transform.position);
    }

}
