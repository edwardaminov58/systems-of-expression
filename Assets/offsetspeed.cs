using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offsetspeed : MonoBehaviour
{
    Material mat;
    //public float scrollLeft;
    //public float scrollRight;
    float offset;
    Vector2 scrollvelocity;
    public GameObject bird;
    public float sidespeed;
    // Start is called before the first frame update
    void Start()
    {
        //Flight = GetComponent<flight>().flight;
        mat = GetComponent<MeshRenderer>().material;
        offset = mat.GetFloat("_offset");
        //scrollvelocity = mat.GetVector("_scroll_velocity");
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        ////offset
        mat.SetFloat("_offset", offset - (bird.transform.position.x / sidespeed));
        //if (x > 0.1)
        //    mat.SetVector("_scroll_velocity", new Vector2(scrollRight, scrollvelocity.y));
        //if (x < -.1)
        //    mat.SetVector("_scroll_velocity", new Vector2(scrollLeft, scrollvelocity.y));
        //gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, (offset - bird.transform.position.x / sidespeed), transform.rotation.z);
    }


}
