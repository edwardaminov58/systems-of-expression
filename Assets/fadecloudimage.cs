using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadecloudimage : MonoBehaviour
{
    // Start is called before the first frame update
    Material mat;
    public GameObject bird;
    public GameObject darkCloudPlane;
    public float startPos;
    public float Threshold;
    public float speed;
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //if (bird.transform.position.y > (darkCloudPlane.transform.position.y + startPos))
        //{
            float t = (bird.transform.position.y + startPos) / (darkCloudPlane.transform.position.y + Threshold) * speed;
            mat.SetColor("_alpha", Color.Lerp(Color.white, Color.gray, t));
       // }
    }
}
