using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sorting : MonoBehaviour
{
    public GameObject bird;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.transform.position.y >= gameObject.transform.position.y)
            meshRenderer.sortingOrder = 100;
        else
            meshRenderer.sortingOrder = 0;
    }
}
