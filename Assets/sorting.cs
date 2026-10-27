using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sorting : MonoBehaviour
{
    GameObject bird;
    public int startSorting;
    public int endSorting;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("flight");
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.transform.position.y >= gameObject.transform.position.y)
            meshRenderer.sortingOrder = endSorting;
        else
            meshRenderer.sortingOrder = startSorting;
    }
}
