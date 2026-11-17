using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritesorting : MonoBehaviour
{
    GameObject bird;
    public int startSorting;
    public int endSorting;
    public Transform objectTransform;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("flight");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.transform.position.y >= objectTransform.position.y)
        {
            Debug.Log("sorted");
            spriteRenderer.sortingOrder = endSorting;
        }
        else
        {
            Debug.Log("sorted2");
            spriteRenderer.sortingOrder = startSorting;
        }
        }
}
