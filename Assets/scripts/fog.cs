using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour

{
    public Material skyboxmaterial;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveandfog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator moveandfog()
    {
        yield return new WaitForSecondsRealtime(8.5f);
        GetComponent<firstpersonedward>().enabled = true;
        RenderSettings.fog = true;
        RenderSettings.skybox = skyboxmaterial;
    }
}
