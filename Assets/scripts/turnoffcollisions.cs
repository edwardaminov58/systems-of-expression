using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class turnoffcollisions : MonoBehaviour
{
    TilemapCollider2D ultimaCollider;
    // Start is called before the first frame update
    void Start()
    {
        GameObject ultimaforest = GameObject.Find("ultimaforest");
        ultimaCollider = ultimaforest.GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void toggleCollisions()
    {
        if (ultimaCollider.isActiveAndEnabled == true)
            ultimaCollider.enabled = false;
        else
            ultimaCollider.enabled = true;
    }
}
