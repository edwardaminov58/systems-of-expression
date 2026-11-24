using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMat : MonoBehaviour
{
    Material mat;
    public GameObject bird;
    public float altStart;
    public float threshold;
    public float amplitudeEnd;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (bird.transform.position.y - altStart) / threshold;
        mat.SetFloat("_Amplitude", Mathf.Lerp(0, amplitudeEnd, t));
    }
}
