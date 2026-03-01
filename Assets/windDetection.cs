using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class windDetection : MonoBehaviour
{
    public flight Flight;
    public float translateSpeedx;
    public float translateSpeedy;
    public float translateSpeedret;
    public float rotateSpeed;
    public float windPush;
    public GameObject player;
    public GameObject bird;
    public Rigidbody rb;
    LayerMask wind;
    LayerMask windCenter;
    //Vector3 Detect;
    //Vector3[] Detect;
   // List<Vector3> Detect;
    Collider[] hitcolliders;
    Collider[] hitcollidersCenter;
    List<GameObject> collisionObjects;
    int index;
    Vector3 Detect;
    Vector3 DetectCenter;
    Vector3 Distance;
    Vector3 DistanceCenter;
    //var collider;
    // Start is called before the first frame update
    void Start()
    {
        wind = LayerMask.GetMask("wind");
        wind = LayerMask.GetMask("windCenter");
    }

    // Update is called once per frame
    void Update()
    {
        //  hitcollidersCenter = (Physics.OverlapSphere(player.transform.position, 50f, windCenter));
        //foreach (var hitcolliderC in hitcollidersCenter)
        //{
        //    DetectCenter = hitcolliderC.ClosestPoint(player.transform.position);

        //}
        //DistanceCenter = DetectCenter - new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //if (DistanceCenter.magnitude > 0 && DistanceCenter.magnitude < 50f)
        //{
        //    float Rot = rotateSpeed / (DistanceCenter.magnitude + 1);
        //    if (DistanceCenter.x > 0)
        //    {
        //        bird.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 100), Rot);
        //    }
        //    else if (DistanceCenter.x < 0)
        //    {
        //        bird.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -100), Rot);
        //    }
        //}
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
       // if (Physics.CheckSphere(player.transform.position, 50f, wind))
        //{
            Distance = Detect - new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
           
            if (Distance.magnitude > 0&&Distance.magnitude < 50f)
            {
            float x = translateSpeedx / (Distance.magnitude + 1);
            float y = translateSpeedy / (Distance.magnitude + 1);
            float Rot = rotateSpeed / (Distance.magnitude + 1);
            bird.transform.position = new Vector3(Mathf.Lerp(player.transform.position.x, -Detect.x, x), Mathf.Lerp(player.transform.position.y, -Detect.y, y), 0);
            //Debug.Log("Detectforce");
            if (Distance.x > 0)
            {
               
                    //Flight.TiltL = Mathf.Lerp(0, 1, Rot);
                    rb.AddForce(new Vector3((-(windPush/(Distance.magnitude+1))), Distance.magnitude * -1, 0));
                    Debug.Log("x = " + x);
                    //bird.transform.Translate(-Distance.magnitude,0, 0 * Time.deltaTime/translateSpeed);
                    Debug.Log("lerp =" + Mathf.Lerp(bird.transform.localPosition.x, -Detect.x, x));
                /* * Time.deltaTime * translateSpeed;*/

                bird.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 10), Rot);
                // bird.transform.position = Vector3.MoveTowards(bird.transform.position, Vector3.Lerp(bird.transform.position, -Detect, t), translateSpeed);
                //bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition,-Detect, translateSpeed * Time.deltaTime);

            }
                else if (Distance.x < 0)
                {
                
                //float t = 1 / Distance.magnitude * 10;
                rb.AddForce(new Vector3(((windPush / (Distance.magnitude + 1))), Distance.magnitude * -1, 0));
                //Debug.Log("t = " + t);
                bird.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -10), Rot);
                //bird.transform.Translate(Vector3.Lerp(Vector3.zero, -Detect, t));
                //bird.transform.Translate(Distance.magnitude, 0, 0 * Time.deltaTime/translateSpeed);
                //bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, Vector3.Lerp(bird.transform.localPosition, -Detect, t), translateSpeed * Time.deltaTime);
                // bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, -Detect, translateSpeed * Time.deltaTime);

            }

            }
        else
        {
            
            Detect = (Vector3.zero);
            Debug.Log("DetectZero");
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, player.transform.position, translateSpeedret * Time.deltaTime);
        }
        Debug.Log("Detect:" + Detect + "Distance: " + Distance + "Distmag: " + Distance.magnitude);
       // }
       
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
        Gizmos.DrawLine(DetectCenter, bird.transform.position);
    }

}
