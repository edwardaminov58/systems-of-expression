using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerCulling : MonoBehaviour
{
    public float[] distances;
    public GameObject bird;
    public float occlusionPlaneStart;
    public float occlusionPlaneEnd;
    public float farPlaneThreshold;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Camera>().layerCullSpherical = true;
        //distances = new float[32];

    }

    // Update is called once per frame
    void Update()
    {
        Occlusion();
        GetComponent<Camera>().layerCullDistances = distances;

    }
    void Occlusion()
    {
        float t = bird.transform.localPosition.y / farPlaneThreshold;
        distances[0] = Mathf.Lerp(occlusionPlaneStart, occlusionPlaneEnd, t);
        //Debug.Log(GetComponent<Camera>().layerCullDistances[0]);
    }
}
