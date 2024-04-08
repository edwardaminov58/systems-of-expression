using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maptransition : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 1);
        StartCoroutine(animateMap(2));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator animateMap(float inTime)
    {
        Vector3 toScale = new Vector3(1, 1, 1);
        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
        {
            transform.localScale = Vector3.Lerp(gameObject.transform.localScale, toScale, t);
            //Debug.Log ("MF: " + t + " / " + transform.position + " / " + currentPosition + " / " + toPosition);d
            //Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, Mathf.SmoothStep (0, 1, t))));
            yield return null;
        }
        transform.localScale = toScale;
    }
}
