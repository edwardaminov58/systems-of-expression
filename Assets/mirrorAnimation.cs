using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorAnimation : MonoBehaviour
{
    public GameObject bird;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = bird.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().flipX = bird.GetComponent<SpriteRenderer>().flipX;
    }
}
