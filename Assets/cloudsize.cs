using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudsize : MonoBehaviour
{
    public GameObject bird;
    public float altStart;
    public float Threshold;
    Material mat;
    public Vector2 size1;
    public Vector2 size2;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (bird.transform.localPosition.y - altStart) / Threshold;
        mat.SetVector("_tiling", Vector2.Lerp(size1, size2, t));
    }
}
