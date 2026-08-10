using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offsetspeed : MonoBehaviour
{
    Material mat;
    float offset;
    public GameObject bird;
    public float sidespeed;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_offset", offset - bird.transform.position.x / sidespeed);
    }
}
