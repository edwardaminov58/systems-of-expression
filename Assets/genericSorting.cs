using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericSorting : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public int sortingOrder;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.sortingOrder = sortingOrder;
    }   
}
