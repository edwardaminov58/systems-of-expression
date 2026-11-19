using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    public GameObject player;
    float n;
    public float shadowPush;
    public Transform plane;
    public Vector3 startScale;
    public Vector3 endScale;
    public float maxScaleHeight;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        n = (player.transform.position.y - plane.position.y)* shadowPush;
        transform.position = new Vector3(player.transform.position.x, plane.position.y, player.transform.position.z + n);
        
        //this is for the cloud shadows
        float t = (player.transform.position.y - plane.position.y)/ maxScaleHeight;
        transform.localScale = Vector3.Lerp(startScale, endScale, t);
        Debug.Log("shadowsize = " + t);
    }
}
