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
    bool colorChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHeight = bird.transform.position.y;
        StartCoroutine(heightSecond());
        recttransform = gameObject.GetComponent<RectTransform>();
        startHeight = recttransform.transform.position.y;
        image = gameObject.GetComponent<Image>();
        //StartCoroutine(colorChange());

    }

    // Update is called once per frame
    void Update()
    {
        recttransform.position = new Vector3(recttransform.position.x, startHeight - bird.transform.position.y * 10.5f, recttransform.position.z);
        //debug.Log("height"+ bird.transform.position.y);
        if (!colorChanged)
        {
            if (bird.transform.position.y > currentHeight + 20f || bird.transform.position.y < currentHeight - 20f)
            {
                StartCoroutine(colorChange());
                colorChanged = true;
            }
        }


    }

    IEnumerator heightSecond()
    {
        while (true)
        {
            currentHeight = bird.transform.position.y;
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator colorChange()
    {
        

                for (float t = 0f; t < 1f; t += Time.deltaTime / 2f)
                {
                    image.color = Color.Lerp(image.color, Color.white, t);
                    yield return null;
                }
                Debug.Log("white");
        StartCoroutine(fadeAway());
            

            //yield return new WaitForSeconds(1.5f);
            //currentHeight = bird.transform.position.y;

  

      
    }

    IEnumerator fadeAway()
    {
        if (bird.transform.position.y <= currentHeight + 5f && bird.transform.position.y >= currentHeight - 5f)
        {
            for (float t = 0f; t < 1f; t += Time.deltaTime / 2f)
            {
                image.color = Color.Lerp(image.color, Color.clear, t);
                yield return null;
                
            }
            colorChanged = false;
            Debug.Log("clear");
            yield break;

        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(fadeAway());
            Debug.Log("repeat");

        }
        

    }
}
