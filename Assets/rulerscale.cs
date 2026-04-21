using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rulerscale : MonoBehaviour
{
    RectTransform recttransform;
    public GameObject bird;
    float startHeight;
    Image image;
    float currentHeight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(heightSecond());
        recttransform = gameObject.GetComponent<RectTransform>();
        startHeight = recttransform.transform.position.y;
        image = gameObject.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        recttransform.position = new Vector3(recttransform.position.x, startHeight - bird.transform.position.y*10, recttransform.position.z);
        Debug.Log("height"+ bird.transform.position.y);
        if (bird.transform.position.y > currentHeight + 2f)
            image.color = Color.white;
        else
            image.color = Color.clear;
          
    }

    IEnumerator heightSecond()
    {
        while (true)
        {
            currentHeight = bird.transform.position.y;
            yield return new WaitForSeconds(.5f);
        }
    }
}
