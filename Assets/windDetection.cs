using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class windDetection : MonoBehaviour
{
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
        if (Physics.CheckSphere(player.transform.position, 40f, wind))
        {
            Distance = Detect - new Vector3(bird.transform.position.x, player.transform.position.y, player.transform.position.z);
            if (Distance.magnitude > 0&&Distance.magnitude < 50f)
            {
                //Debug.Log("Detectforce");
                if (Distance.x > 0)
                {
                    rb.AddForce(new Vector3((-Distance.magnitude * windPush), Distance.magnitude * -1, 0));
                    //bird.transform.localPosition+= new Vector3(Distance.magnitude, Distance.magnitude, 0);
                }
                else if (Distance.x < 0)
                {
                    rb.AddForce(new Vector3((Distance.magnitude * windPush), Distance.magnitude * -1, 0));
                    //bird.transform.localPosition += new Vector3(Distance.magnitude, Distance.magnitude, 0);
                }
            }
            Debug.Log("Detect:" + Detect + "Distance: " + Distance + "Distmag: " + Distance.magnitude);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(bird.transform.position, 50f);
        // if (Physics.CheckSphere(player.transform.position, 50f, wind))
        //{
        //foreach (Vector3 derect in Detect) { 
        //    Gizmos.DrawLine(derect, player.transform.position);
        Gizmos.DrawLine(Detect, bird.transform.position);
    }

}
