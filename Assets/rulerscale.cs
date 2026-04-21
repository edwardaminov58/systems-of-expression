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
        if (bird.transform.position.y > currentHeight + 10f)
            StartCoroutine(colorChange());


    }

    IEnumerator heightSecond()
    {
        while (true)
        {
            currentHeight = bird.transform.position.y;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator colorChange()
    {

            for (float t = 0f; t < 1f; t += Time.deltaTime / 1f)
            {
                image.color = Color.Lerp(image.color, Color.white, t);
                yield return null;
            }

        yield return new WaitForSeconds(1.5f);

        if (currentHeight <= bird.transform.position.y +5f)
        {
            for (float t = 0f; t < 1f; t += Time.deltaTime / 2f)
            {
                image.color = Color.Lerp(image.color, Color.clear, t);
                yield return null;
            }
        }
      
    

      
    }
}
