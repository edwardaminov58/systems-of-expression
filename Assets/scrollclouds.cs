using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollclouds : MonoBehaviour
{
    Material mat;
    MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = mat.GetTextureOffset("_MainTex");
        offset -= new Vector2(0, 1);
        //offset.x += x * Time.deltaTime/15;
        //offset.x = bird.transform.localPosition.x / 1500;

        mat.SetTextureOffset("_MainTex", offset);
    }
}
